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
    
    public typealias AuthenticateCallback = (Error?, Token?) -> ()
    public func authenticate(username: String, password: String, callback: @escaping AuthenticateCallback) {
        let builder: AuthenticationHeaderBuilder = AuthenticationHeaderBuilder();
        builder.username = username;
        builder.password = password;
        builder.authType = .basic;
        
        var headers: Headers = [:];
        headers["Authorization"] = try! builder.build();
        
        requestable?.uploadJSON(url: urlManager.getToken(), headers: headers, object: EmptyRequestBody(), onCompletion: { (data: Data) in
            guard let token: Token = self.decodeJson(data: data) else {
                callback(nil, nil); // TODO: Error
                return;
            }
            
            self.token = token;
            callback(nil, token);
        }, onError: { (error: RequestError) in
            callback(error.error!, nil);
        });
    }
    
    public typealias GetNotebooksCallback = ([Notebook]?, Error?) -> ()
    public func getNotebooks(callback: @escaping GetNotebooksCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebooks(), headers: defaultHeaders, onCompletion: { (response: Data) in
            guard let list: ListResponse<NotebookModel> = self.decodeJson(data: response) else {
                callback(nil, nil); // TODO: Error
                return;
            }
            
            let notebooks: [Notebook] = list.list.map({ (model: NotebookModel) -> Notebook in
                return model.notebook;
            });
            callback(notebooks, nil);
        }, onError: { (error: RequestError) in
            callback(nil, error.error!);
        });
    }
    
    public typealias GetNotebookCallback = (Notebook?, Error?) -> ()
    public func getNotebook(id: Int, callback: @escaping GetNotebookCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebook(id: id), headers: defaultHeaders, onCompletion: { (response: Data) in
            guard let notebook: NotebookModel = self.decodeJson(data: response) else {
                callback(nil, nil);
                return;
            }
            
            callback(notebook.notebook, nil);
        }, onError: { (error: RequestError) in
            callback(nil, error.error!);
        });
    }
    
    public typealias CreateNotebookCallback = (Error?) -> ()
    public func createNotebook(notebook: Notebook, callback: @escaping CreateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.createNotebook(), headers: defaultHeaders, object: notebook.webModel, onCompletion: { (data: Data) in
            
        }, onError: { (error: RequestError) in
            
        });
    }
    
    public typealias DeleteNotebookCallback = (Error?) -> ()
    public func deleteNotebook(id: Int, callback: @escaping DeleteNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.deleteNotebook(id: id), headers: defaultHeaders, object: EmptyRequestBody(), onCompletion: { (data: Data) in
            
        }, onError: { (error: RequestError) in
            
        });
    }
    
    public typealias UpdateNotebookCallback = (Error?) -> ()
    public func updateNotebook(notebook: Notebook, id: Int, callback: @escaping UpdateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.updateNotebook(id: id), headers: defaultHeaders, object: notebook.webModel, onCompletion: { (data: Data) in

        }, onError: { (error: RequestError) in

        });
    }
    
    private func decodeJson<T>(data: Data) -> T? where T : Codable {
        let dateFormatter: DateFormatter = DateFormatter();
        dateFormatter.dateFormat = Token.dateFormat;    // Just use token for the ModelBase implementation
        
        let decoder: JSONDecoder = JSONDecoder();
        decoder.dateDecodingStrategy = .formatted(dateFormatter);
        return try? decoder.decode(T.self, from: data);
    }
    
    private lazy var defaultHeaders: Headers = {
        var headers: Headers = [:];
        headers["Authorization"] = buildTokenHeader();
        headers["ContentType"] = "application/json";
        
        return headers;
    } ();
    
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
