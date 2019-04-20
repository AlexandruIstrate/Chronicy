//
//  WebAPIRequest.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 19/04/2019.
//

import Foundation;

struct AuthRequest: Codable {
    var username: String;
    var password: String;
    
    init(from: AuthenticationInfo) {
        self.username = from.username;
        self.password = from.password;
    }
}

protocol AuthenticatedRequest {
    var token: String { get set }
}

struct NotebookInfoAllRequest: AuthenticatedRequest, Codable {
    var token: String;
}

struct NotebookInfoRequest: AuthenticatedRequest, Codable {
    var token: String;
    var id: String;
}

struct NotebookRequest: AuthenticatedRequest, Codable {
    var token: String;
    var id: String;
}
