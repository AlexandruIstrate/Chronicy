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
    func request(with json: String, url: URL, type: RequestType, onCompletion: @escaping RequestCompletionCallback, onError: @escaping RequestErrorCallback) {
        var method: HTTPMethod = .get;
        
        if let userMethod: HTTPMethod = HTTPMethod(rawValue: type.rawValue.uppercased()) {
            method = userMethod;
        } else {
            Log.error(message: "Could not determine HTTPMethod for AlamofireRequestable! Defaulting to GET.");
        }
        
        Alamofire.request(url, method: method)
            .validate()
            .responseJSON { (response: DataResponse<Any>) in
                switch response.result {
                case .success(let value):
                    print(value);
                    
                case .failure(let error):
                    onError(.generalFailure(reason: error.localizedDescription));
                }
            }
    }
}
