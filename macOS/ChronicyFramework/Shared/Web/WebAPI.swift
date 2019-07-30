//
//  WebAPI.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class WebAPI {
    public static let shared: WebAPI = WebAPI();
    
    public var requestable: Requestable?;
    private var urlManager: URLManager = URLManager(baseURL: Settings.webServiceURL);
    
    private var token: Token?;
    
    public var url: String {
        get { return urlManager.baseURL; }
        set { urlManager.baseURL = newValue; }
    }
    
    public typealias AuthenticateCallback = (APIError?, Token?) -> ()
    public func authenticate(username: String, password: String, callback: @escaping AuthenticateCallback) {
        let builder: AuthenticationHeaderBuilder = AuthenticationHeaderBuilder();
        builder.username = username;
        builder.password = password;
        builder.authType = .basic;
        
        var headers: Headers = [:];
        headers["Authorization"] = try! builder.build();
        
        requestable?.uploadJSON(url: urlManager.getToken(), object: EmptyRequestBody(), headers: headers, requestMethod: .post, onCompletion: { (data: Data) in
            guard let token: Token = self.decodeJson(data: data) else {
                callback(APIError.authenticationFailure, nil);
                return;
            }
            
            self.token = token;
            callback(nil, token);
        }, onError: { (error: RequestError) in
            callback(APIError.authenticationFailure, nil);
        });
    }
    
    public typealias GetNotebooksCallback = ([Notebook]?, APIError?) -> ()
    public func getNotebooks(callback: @escaping GetNotebooksCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebooks(), headers: defaultHeaders, onCompletion: { (response: Data) in
            let json: String? = String(bytes: response, encoding: .utf8);
            print(json ?? "No JSON response");
            
            guard let list: ListResponse<NotebookModel> = self.decodeJson(data: response) else {
                callback(nil, APIError.readAllFailure); // TODO: Error
                return;
            }
            
            let notebooks: [Notebook] = list.list.map({ (model: NotebookModel) -> Notebook in
                return model.notebook;
            });
            
            callback(notebooks, nil);
        }, onError: { (error: RequestError) in
            callback(nil, APIError.readAllFailure);
        });
    }
    
    public typealias GetNotebookCallback = (Notebook?, APIError?) -> ()
    public func getNotebook(id: Int, callback: @escaping GetNotebookCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebook(id: id), headers: defaultHeaders, onCompletion: { (response: Data) in
            guard let notebook: NotebookModel = self.decodeJson(data: response) else {
                callback(nil, APIError.readFailure);
                return;
            }
            
            callback(notebook.notebook, nil);
        }, onError: { (error: RequestError) in
            callback(nil, APIError.readFailure);
        });
    }
    
    public typealias CreateNotebookCallback = (APIError?) -> ()
    public func createNotebook(notebook: Notebook, callback: @escaping CreateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.createNotebook(), object: notebook.webModel, headers: defaultHeaders, requestMethod: .post, onCompletion: { (data: Data) in
            callback(nil);
        }, onError: { (error: RequestError) in
            callback(APIError.createFailure);
        });
    }
    
    public typealias DeleteNotebookCallback = (APIError?) -> ()
    public func deleteNotebook(id: Int, callback: @escaping DeleteNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.deleteNotebook(id: id), object: EmptyRequestBody(), headers: defaultHeaders, requestMethod: .delete, onCompletion: { (data: Data) in
            callback(nil);
        }, onError: { (error: RequestError) in
            callback(APIError.deleteFailure);
        });
    }
    
    public typealias UpdateNotebookCallback = (APIError?) -> ()
    public func updateNotebook(notebook: Notebook, id: Int, callback: @escaping UpdateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.updateNotebook(id: id), object: notebook.webModel, headers: defaultHeaders, requestMethod: .put, onCompletion: { (data: Data) in
            callback(nil);
        }, onError: { (error: RequestError) in
            callback(APIError.updateFailure);
        });
    }
    
    private func decodeJson<T>(data: Data) -> T? where T : Codable {
        let dateFormatter: DateFormatter = DateFormatter();
        dateFormatter.dateFormat = Token.dateFormat;    // Just use Token for the ModelBase implementation
        
        let decoder: JSONDecoder = JSONDecoder();
        decoder.dateDecodingStrategy = .formatted(dateFormatter);
        
        do {
            return try decoder.decode(T.self, from: data);
        } catch (let e) {
            print(e);
            
            let json: String = String(bytes: data, encoding: .utf8) ?? "Cannot convert data";
//            print(json);
            
            return nil;
        }
    }
    
    private var defaultHeaders: Headers {
        var headers: Headers = [:];
        headers["Authorization"] = self.buildTokenHeader();
        headers["ContentType"] = "application/json";
        
        return headers;
    };
    
    private func buildTokenHeader() -> String {
        guard let token: String = token?.accessToken else {
            Log.error(message: "No token present");
            return "";
        }
        
        let builder: AuthenticationHeaderBuilder = AuthenticationHeaderBuilder();
        builder.key = token;
        builder.authType = .token;
        return try! builder.build();
    }
    
    public struct EmptyRequestBody : Codable {
        
    }
}

public enum APIError: Error {
    case authenticationFailure;
    
    case createFailure;
    case readFailure;
    case readAllFailure;
    case updateFailure;
    case deleteFailure;
}
