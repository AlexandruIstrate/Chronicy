//
//  WebAPIResponseInfo.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 20/04/2019.
//

import Foundation;

struct AuthResponseInfo: Codable {
    let token: String;
    let expiryDate: Date;
}
