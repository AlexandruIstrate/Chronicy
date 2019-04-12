//
//  InputTemplate.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/04/2019.
//

import Foundation;

public class InputTemplate {
    
    public var name: String;
    public var fields: [CustomField];
    
    public init(name: String, fields: [CustomField] = []) {
        self.name = name;
        self.fields = [];
    }

}
