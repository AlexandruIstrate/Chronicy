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
    
    public func authenticate(auth: AuthenticationInfo) {
        do {
            let request: AuthRequestInfo = AuthRequestInfo(from: auth);
            try requestManager.post(with: request, url: urlManager[.auth], onCompletion: { (object: Codable?, data: Data?) in
                
            }, onError: { (error: RequestError) in
                
            });
        } catch {
            
        }
    }
    
    public func getInfoForAll() -> [NotebookInfo] {
        return [];
    }
    
    public func getInfo(id: String) -> NotebookInfo? {
        return nil;
    }
    
    public func getNotebook(info: NotebookInfo) -> Notebook {
        return Notebook(name: "Placeholder");
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
