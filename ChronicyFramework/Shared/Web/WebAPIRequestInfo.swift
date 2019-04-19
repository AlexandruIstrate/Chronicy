//
//  WebAPIRequestInfo.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 19/04/2019.
//

import Foundation;

struct AuthRequestInfo: Codable {
    var username: String;
    var password: String;
    
    init(from: AuthenticationInfo) {
        self.username = from.username;
        self.password = from.password;
    }
}


