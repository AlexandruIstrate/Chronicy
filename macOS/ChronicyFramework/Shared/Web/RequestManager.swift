//
//  RequestManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 06/04/2019.
//

import Foundation;

public protocol Requestable {
    func downloadData(url: String, headers: Headers?, onCompletion: @escaping RequestDownloadCallback, onError: @escaping RequestErrorCallback);
    func uploadData(url: String, data: Data, headers: Headers?, requestMethod: RequestMethod?, onCompletion: @escaping RequestUploadCallback, onError: @escaping RequestErrorCallback);
    
    func downloadJSON(url: String, headers: Headers?, onCompletion: @escaping RequestJSONDownloadCallback, onError: @escaping RequestErrorCallback);
    func uploadJSON<T : Codable>(url: String, object: T, headers: Headers?, requestMethod: RequestMethod?, onCompletion: @escaping RequestJSONUploadCallback, onError: @escaping RequestErrorCallback);
}

public typealias RequestDownloadCallback = (Data) -> ();
public typealias RequestUploadCallback = (Data) -> ();

public typealias RequestJSONDownloadCallback = (Data) -> ();
public typealias RequestJSONUploadCallback = (Data) -> ();

public typealias RequestErrorCallback = (RequestError) -> ();

public typealias Headers = [String : String];

public enum RequestMethod: String {
    case get        = "GET";
    case post       = "POST";
    case put        = "PUT"
    case update     = "UPDATE";
    case delete     = "DELETE";
    // TODO: Add the others
}

public struct RequestError {
    public var errorCode: Int;
    public var message: String;
    
    public var error: Error?;
    
    public init(errorCode: Int, message: String) {
        self.errorCode = errorCode;
        self.message = message;
    }
    
    public init(error: Error) {
        self.error = error;
        self.errorCode = -1;
        self.message = error.localizedDescription;
    }
}
