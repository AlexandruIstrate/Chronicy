//
//  URLManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 20/04/2019.
//

import Foundation;

public class URLManager {
    
    public var baseURL: String;
    
    public init(baseURL: String) {
        self.baseURL = baseURL;
    }
    
    public func getToken() -> String
    {
        return "\(baseURL)/auth";
    }
    
    public func getNotebooks() -> String
    {
        return "\(baseURL)/notebook/all";
    }
    
    public func getNotebook(id: Int) -> String
    {
        return "\(baseURL)/notebook?id=\(id)";
    }
    
    public func createNotebook() -> String
    {
        return "\(baseURL)/notebook/create";
    }
    
    public func deleteNotebook(id: Int) -> String
    {
        return "\(baseURL)/notebook/delete?id=\(id)";
    }
    
    public func updateNotebook(id: Int) -> String
    {
        return "\(baseURL)/notebook/update?id=\(id)";
    }
}
