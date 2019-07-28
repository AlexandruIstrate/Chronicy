//
//  ModelBase.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public protocol ModelBase: Codable {
    var errorCode: Int { get set }
    var errorMessage: String? { get set }
    
    var statusCode: Int { get set }
}

extension ModelBase {
    public static var dateFormat: String {
        return "yyyy-MM-dd'T'HH:mm:ss";
    }
}
