//
//  ExtensionStack.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/04/2019.
//

import Foundation;

public struct ExtensionStack: Equatable, Codable, FieldContainer {
    public var name: String;
    public var fields: [ExtensionField];
    
    public init(name: String, fields: [ExtensionField]) {
        self.name = name;
        self.fields = fields;
    }
}
