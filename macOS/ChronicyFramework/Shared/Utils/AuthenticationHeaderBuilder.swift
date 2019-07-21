//
//  AuthenticationHeaderBuilder.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 17/07/2019.
//

import Foundation;

public class AuthenticationHeaderBuilder {
    
    public var password: String?;
    public var username: String?;
    
    public var key: String?;
    
    public var authType: AuthenticationType = .basic;
    
    public func build() throws -> String {
        switch authType {
        case .basic:
            return try buildBasic();
        case .token:
            return try buildKey();
        }
    }
    
    private func buildBasic() throws -> String {
        guard let username: String = username else {
            throw AuthenticationHeaderBuilderError.missingProperty(name: "username");
        }
        
        guard let password: String = password else {
            throw AuthenticationHeaderBuilderError.missingProperty(name: "password");
        }
        
        let combined: String = username + ":" + password;
        
        guard let encoded: String = encode(str: combined) else {
            throw AuthenticationHeaderBuilderError.encodeFailed;
        }
        
        return "\(authType.rawValue) \(encoded)"
    }
    
    private func buildKey() throws -> String {
        guard let key: String = key else {
            throw AuthenticationHeaderBuilderError.missingProperty(name: "key");
        }
        
        guard let encoded: String = encode(str: key) else {
            throw AuthenticationHeaderBuilderError.encodeFailed;
        }
        
        return "\(authType.rawValue) \(encoded)"
    }
    
    private func encode(str: String) -> String? {
        guard let utf8data: Data = str.data(using: .utf8) else {
            return nil;
        }
        
        return utf8data.base64EncodedString();
    }
}

public enum AuthenticationType : String {
    case basic = "Basic";
    case token = "Token"
}

public enum AuthenticationHeaderBuilderError : Error {
    case missingProperty(name: String);
    case encodeFailed;
}
