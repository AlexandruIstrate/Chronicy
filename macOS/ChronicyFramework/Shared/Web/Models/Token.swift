//
//  Token.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class Token: ModelBase {
    // ModelBase Start
    
    public var errorCode: Int = 0;
    public var errorMessage: String?;
    
    public var statusCode: Int = 200;
    
    // ModelBase End
    
    public var accessToken: String = "";
    public var expirationDate: Date = Date();
    
    public var expired: Bool { return expirationDate <= Date(); }
}
