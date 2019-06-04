//
//  WebAPIResponse.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 20/04/2019.
//

import Foundation;

public struct AuthResponse: Codable {
    let token: String;
    let expiryDate: Date;
}

public struct NotebookInfoResponse: Codable {
    let name: String;
    let id: String;
    let dateCreated: Date;
}

public struct NotebookInfoAllResponse: Codable {
    let notebooks: [NotebookInfoResponse];
}

//extension Notebook: Codable {
//    
//}
