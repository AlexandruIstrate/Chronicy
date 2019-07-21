//
//  WebAPI.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class WebAPI {
    
//    private var requestManager: RequestManager = RequestManager();
    
    private var requestable: Requestable?;
    private var urlManager: URLManager = URLManager(baseURL: "https://192.168.100.5/api");
    
    private var token: String?;
    
    public typealias AuthenticateCallback = (Error?) -> ()
    public func authenticate(username: String, password: String, callback: @escaping AuthenticateCallback) {
        let builder: AuthenticationHeaderBuilder = AuthenticationHeaderBuilder();
        builder.username = username;
        builder.password = password;
        builder.authType = .basic;
        
        var headers: Headers = [:];
        headers["Authorization"] = try! builder.build();
        
        requestable?.downloadJSON(url: urlManager.getToken(), headers: headers, onCompletion: { (response: Codable) in
            callback(nil);
        }, onError: { (error: RequestError) in
            callback(error.error!);
        });
    }
    
    public typealias GetNotebooksCallback = ([Notebook]?, Error?) -> ()
    public func getNotebooks(callback: @escaping GetNotebooksCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebooks(), headers: defaultHeaders, onCompletion: { (response: Data) in
            let decoder: JSONDecoder = JSONDecoder();
            //decoder.decode(ListResponse<Notebook>.self, from: response);
        }, onError: { (error: RequestError) in
            callback(nil, error.error!);
        });
    }
    
    public typealias GetNotebookCallback = (Notebook?, Error?) -> ()
    public func getNotebook(id: Int, callback: @escaping GetNotebookCallback) {
        requestable?.downloadJSON(url: urlManager.getNotebook(id: id), headers: defaultHeaders, onCompletion: { (response: Data) in
            let decoder: JSONDecoder = JSONDecoder();
            //decoder.decode(Notebook.self, from: response);
        }, onError: { (error: RequestError) in
            callback(nil, error.error!);
        });
    }
    
    public typealias CreateNotebookCallback = (Error?) -> ()
    public func createNotebook(notebook: Notebook, callback: @escaping CreateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.createNotebook(), headers: defaultHeaders, object: notebook, onCompletion: { (data: Data) in
            
        }, onError: { (error: RequestError) in
            
        })
    }
    
    public typealias DeleteNotebookCallback = (Error?) -> ()
    public func deleteNotebook(id: Int, callback: @escaping DeleteNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.deleteNotebook(id: id), headers: defaultHeaders, object: EmptyRequestBody(), onCompletion: { (data: Data) in
            
        }, onError: { (error: RequestError) in
            
        })
    }
    
    public typealias UpdateNotebookCallback = (Error?) -> ()
    public func updateNotebook(notebook: Notebook, id: Int, callback: @escaping UpdateNotebookCallback) {
        requestable?.uploadJSON(url: urlManager.updateNotebook(id: id), headers: defaultHeaders, object: notebook, onCompletion: { (data: Data) in
            
        }, onError: { (error: RequestError) in
            
        })
    }
    
    private lazy var defaultHeaders: Headers = {
        var headers: Headers = [:];
        headers["Authorization"] = buildTokenHeader();
        headers["ContentType"] = "application/json";
        
        return headers;
    } ();
    
    private func buildTokenHeader() -> String {
        guard let token: String = token else {
            return "";
        }
        
        let builder: AuthenticationHeaderBuilder = AuthenticationHeaderBuilder();
        builder.key = token;
        builder.authType = .token;
        return try! builder.build();
    }
    
//    public typealias WebAPIAuthCallback = (AuthResponse?, RequestError?) -> ();
//    public func authenticate(auth: AuthenticationInfo, callback: @escaping WebAPIAuthCallback) {
//        do {
//            let request: AuthRequest = AuthRequest(from: auth);
//            try requestManager.post(with: request, url: urlManager[.auth], onCompletion: { (data: Data?) in
//                guard let data: Data = data else {
//                    callback(nil, RequestError.invalidResponse);
//                    return;
//                }
//
//                self.decodeResponse(data: data, callback: callback);
//            }, onError: { (error: RequestError) in
//                callback(nil, error);
//            });
//        } catch {
//            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
//        }
//    }
//
//    public typealias WebAPIInfoCallback = (NotebookInfoResponse?, RequestError?) -> ();
//    public func getInfo(token: String, info: NotebookInfo, callback: @escaping WebAPIInfoCallback) {
//        do {
//            let request: NotebookInfoRequest = NotebookInfoRequest(token: token, id: info.id);
//            try requestManager.post(with: request, url: urlManager[.infoNotebook], onCompletion: { (data: Data?) in
//                guard let data: Data = data else {
//                    callback(nil, RequestError.invalidResponse);
//                    return;
//                }
//
//                self.decodeResponse(data: data, callback: callback);
//            }, onError: { (error: RequestError) in
//                callback(nil, error);
//            })
//        } catch {
//            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
//        }
//    }
//
//    public typealias WebAPIInfoAllCallback = (NotebookInfoAllResponse?, RequestError?) -> ();
//    public func getInfoForAll(token: String, callback: @escaping WebAPIInfoAllCallback) {
//        do {
//            let request: NotebookInfoAllRequest = NotebookInfoAllRequest(token: token);
//            try requestManager.post(with: request, url: urlManager[.infoAllNotebooks], onCompletion: { (data: Data?) in
//                guard let data: Data = data else {
//                    callback(nil, RequestError.invalidResponse);
//                    return;
//                }
//
//                self.decodeResponse(data: data, callback: callback);
//            }, onError: { (error: RequestError) in
//                callback(nil, error);
//            });
//        } catch {
//            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
//        }
//    }
//
//    public typealias WebAPINotebookCallback = (Notebook?, RequestError?) -> ();
//    public func getNotebook(token: String, info: NotebookInfo, callback: @escaping WebAPINotebookCallback) {
//        do {
//            let request: NotebookRequest = NotebookRequest(token: token, id: info.id);
//            try requestManager.post(with: request, url: urlManager[.notebook], onCompletion: { (data: Data?) in
//                guard let data: Data = data else {
//                    callback(nil, RequestError.invalidResponse);
//                    return;
//                }
//
//                // TODO: Make it work
////                self.decodeResponse(data: data, callback: callback);
//            }, onError: { (error: RequestError) in
//                callback(nil, error);
//            });
//        } catch {
//            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
//        }
//    }
//
//    private func decodeResponse<T: Codable>(data: Data, callback: (T?, RequestError?) -> ()) {
//        let decoder: JSONDecoder = JSONDecoder();
//
//        guard let response: T = try? decoder.decode(T.self, from: data) else {
//            callback(nil, RequestError.invalidResponse);
//            return;
//        }
//
//        callback(response, nil);
//    }
    
    public struct EmptyRequestBody : Codable {
        
    }
    
    public struct ListResponse<T> : Codable where T : Codable {
        public var items: [T];
    }
}
