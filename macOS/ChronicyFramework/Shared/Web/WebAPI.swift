//
//  WebAPI.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class WebAPI {
    
    private var requestManager: RequestManager = RequestManager();
    private var urlManager: URLManager = URLManager();
    
    public typealias WebAPIAuthCallback = (AuthResponse?, RequestError?) -> ();
    public func authenticate(auth: AuthenticationInfo, callback: @escaping WebAPIAuthCallback) {
        do {
            let request: AuthRequest = AuthRequest(from: auth);
            try requestManager.post(with: request, url: urlManager[.auth], onCompletion: { (data: Data?) in
                guard let data: Data = data else {
                    callback(nil, RequestError.invalidResponse);
                    return;
                }
                
                self.decodeResponse(data: data, callback: callback);
            }, onError: { (error: RequestError) in
                callback(nil, error);
            });
        } catch {
            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
        }
    }
    
    public typealias WebAPIInfoCallback = (NotebookInfoResponse?, RequestError?) -> ();
    public func getInfo(token: String, info: NotebookInfo, callback: @escaping WebAPIInfoCallback) {
        do {
            let request: NotebookInfoRequest = NotebookInfoRequest(token: token, id: info.id);
            try requestManager.post(with: request, url: urlManager[.infoNotebook], onCompletion: { (data: Data?) in
                guard let data: Data = data else {
                    callback(nil, RequestError.invalidResponse);
                    return;
                }
                
                self.decodeResponse(data: data, callback: callback);
            }, onError: { (error: RequestError) in
                callback(nil, error);
            })
        } catch {
            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
        }
    }
    
    public typealias WebAPIInfoAllCallback = (NotebookInfoAllResponse?, RequestError?) -> ();
    public func getInfoForAll(token: String, callback: @escaping WebAPIInfoAllCallback) {
        do {
            let request: NotebookInfoAllRequest = NotebookInfoAllRequest(token: token);
            try requestManager.post(with: request, url: urlManager[.infoAllNotebooks], onCompletion: { (data: Data?) in
                guard let data: Data = data else {
                    callback(nil, RequestError.invalidResponse);
                    return;
                }
                
                self.decodeResponse(data: data, callback: callback);
            }, onError: { (error: RequestError) in
                callback(nil, error);
            });
        } catch {
            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
        }
    }
    
    public typealias WebAPINotebookCallback = (Notebook?, RequestError?) -> ();
    public func getNotebook(token: String, info: NotebookInfo, callback: @escaping WebAPINotebookCallback) {
        do {
            let request: NotebookRequest = NotebookRequest(token: token, id: info.id);
            try requestManager.post(with: request, url: urlManager[.notebook], onCompletion: { (data: Data?) in
                guard let data: Data = data else {
                    callback(nil, RequestError.invalidResponse);
                    return;
                }
                
                // TODO: Make it work
//                self.decodeResponse(data: data, callback: callback);
            }, onError: { (error: RequestError) in
                callback(nil, error);
            });
        } catch {
            callback(nil, RequestError.generalFailure(reason: "The request could not be made!"));
        }
    }
    
    private func decodeResponse<T: Codable>(data: Data, callback: (T?, RequestError?) -> ()) {
        let decoder: JSONDecoder = JSONDecoder();
        
        guard let response: T = try? decoder.decode(T.self, from: data) else {
            callback(nil, RequestError.invalidResponse);
            return;
        }
        
        callback(response, nil);
    }
}

public struct AuthenticationInfo {
    public var username: String;
    public var password: String;
    
    public init(username: String, password: String) {
        self.username = username;
        self.password = password;
    }
}
