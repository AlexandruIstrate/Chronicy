//
//  DefaultFields.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 08/04/2019.
//

import Foundation;

public struct TextField: CustomField {
    public var name: String;
    public var displayName: String = "Text Field";
    public var value: Any?;
    
    public var type: FieldType = .string;
    
    public init(name: String, value: String? = nil) {
        self.name = name;
        self.value = value;
    }
}

public struct NumericField: CustomField {
    public var name: String;
    public var displayName: String = "Numeric Field";
    public var value: Any?;
    
    public var type: FieldType = .number;
    
    public init(name: String, value: Float? = nil) {
        self.name = name;
        self.value = value;
    }
}
