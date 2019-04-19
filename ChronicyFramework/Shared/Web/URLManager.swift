//
//  URLManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 20/04/2019.
//

import Foundation;

public class URLManager {
    
    public private(set) var urlDictionary: [URLKey : URL] = [:];
    
    public var urls: [URL] {
        let result: [URL] = Array<URL>(self.urlDictionary.values);
        return result;
    }
    
    public func set(key: URLKey, url: URL) {
        self.urlDictionary[key] = url;
    }
    
    public func get(key: URLKey) -> URL? {
        return self.urlDictionary[key];
    }
    
    public subscript(key: URLKey) -> URL {
        return self.urlDictionary[key]!;
    }
}

public enum URLKey {
    case auth;
}
