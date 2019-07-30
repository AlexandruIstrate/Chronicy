//
//  AlamofireRequestable.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 06/04/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;
import Alamofire;

class AlamofireRequestable: Requestable {
    
    func downloadData(url: String, headers: Headers?, onCompletion: @escaping RequestDownloadCallback, onError: @escaping RequestErrorCallback) {
        Alamofire.request(url, headers: headers).validate().responseData { (response: DataResponse<Data>) in
            switch response.result {
            case .failure(let error):
                onError(RequestError(errorCode: 3, message: error.localizedDescription));
            case .success(let responseObject):
                onCompletion(responseObject);
            }
        }
    }
    
    func uploadData(url: String, data: Data, headers: Headers?, requestMethod: RequestMethod? = .post, onCompletion: @escaping RequestUploadCallback, onError: @escaping RequestErrorCallback) {
        guard let requestURL: URL = URL(string: url) else {
            onError(RequestError(errorCode: 1, message: "The URL is not valid"));
            return;
        }
        
        var request: URLRequest = URLRequest(url: requestURL);
        request.httpMethod = requestMethod!.rawValue;
        
        Alamofire.request(request).responseData { (response: DataResponse<Data>) in
            switch response.result {
            case .failure(let error):
                onError(RequestError(errorCode: 3, message: error.localizedDescription));
            case .success(let responseObject):
                onCompletion(responseObject);
            }
        }
    }
    
    func downloadJSON(url: String, headers: Headers?, onCompletion: @escaping RequestJSONDownloadCallback, onError: @escaping RequestErrorCallback) {
        Alamofire.request(url, headers: headers).validate().responseJSON { (response: DataResponse<Any>) in
            switch response.result {
            case .failure(let error):
                onError(RequestError(errorCode: 3, message: error.localizedDescription));
            case .success:
                guard let data: Data = response.data else {
                    onError(RequestError(errorCode: 4, message: "Could not decode response"));
                    return;
                }
                onCompletion(data);
                break;
            }
        }
    }
    
    func uploadJSON<T>(url: String, object: T, headers: Headers?, requestMethod: RequestMethod? = .post, onCompletion: @escaping RequestJSONUploadCallback, onError: @escaping RequestErrorCallback) where T : Decodable, T : Encodable {
        guard let requestURL: URL = URL(string: url) else {
            onError(RequestError(errorCode: 1, message: "The URL is not valid"));
            return;
        }
        
        var request: URLRequest = URLRequest(url: requestURL);
        request.httpMethod = requestMethod!.rawValue;
        request.setValue("application/json", forHTTPHeaderField: "Content-Type");
        
        if let headers: Headers = headers {
            request.allHTTPHeaderFields = headers;
        }
        
        do {
            let formatter: DateFormatter = DateFormatter();
            formatter.dateFormat = Token.dateFormat;
                
            let encoder: JSONEncoder = JSONEncoder();
            encoder.dateEncodingStrategy = .formatted(formatter);
                
            let data: Data = try encoder.encode(object);
            request.httpBody = data;
        } catch {
            onError(RequestError(errorCode: 2, message: "The object cannot be serialized to JSON"));
            return;
        }
        
        Alamofire.request(request).responseJSON { (response: DataResponse<Any>) in
            switch response.result {
            case .failure(let error):
                onError(RequestError(errorCode: 3, message: error.localizedDescription));
            case .success:
                onCompletion(response.data!);
            }
        }
    }
}
