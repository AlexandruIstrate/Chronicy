//
//  RequestManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 06/04/2019.
//

import Foundation;

public typealias RequestCompletionCallback = (Codable?, Data?) -> ();
public typealias RequestErrorCallback = (RequestError) -> ();

public protocol Requestable {
    func request(with json: String, url: URL, type: RequestType, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback);
}

public class RequestManager {
    
    public var requestable: Requestable?;
    
    public init() {
        
    }
    
    public func request<T: Codable>(with object: T, url: URL, requestType: RequestType, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) throws {
        do {
            let encoder: JSONEncoder = JSONEncoder();
            let data: Data = try encoder.encode(object);
            
            guard let json: String = String(data: data, encoding: .utf8) else {
                onError(.invalidRequestData);
                return;
            }
            
            self.requestable?.request(with: json, url: url, type: requestType, onCompletion: { (codable: Codable?, data: Data?) in
                
            }, onError: { (error: RequestError) in
                onError(error);
            });
        } catch {
            Log.error(message: "Invalid data for request!");
            throw RequestError.invalidRequestData;
        }
    }
    
    public func get<T: Codable>(with object: T, url: URL, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) throws {
        try self.request(with: object, url: url, requestType: .get, onCompletion: onCompletion, onError: onError);
    }
    
    public func post<T: Codable>(with object: T, url: URL, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) throws {
        try self.request(with: object, url: url, requestType: .post, onCompletion: onCompletion, onError: onError);
    }
    
    public func put<T: Codable>(with object: T, url: URL, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) throws {
        try self.request(with: object, url: url, requestType: .put, onCompletion: onCompletion, onError: onError);
    }
    
    public func delete<T: Codable>(with object: T, url: URL, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) throws {
        try self.request(with: object, url: url, requestType: .delete, onCompletion: onCompletion, onError: onError);
    }
    
}

public enum RequestType: String {
    case get;
    case post;
    case put;
    case delete;
    // TODO: Add the other cases
}

public enum RequestError: Error {
    case invalidRequestData;
    case invalidResponse;
    case generalFailure(reason: String);
}
