//
//  DefaultFields.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 08/04/2019.
//

import Foundation;

public class TextField: CustomField {
    public var name: String;
    public var displayName: String = "Text Field";
    public var value: Any?;
    
    public var type: FieldType = .string;
    
    public var valueChangedCallback: CustomField.FieldOnValueChangedCallback?;
    
    public init(name: String, value: Any? = nil) {
        self.name = name;
        self.value = value;
    }
}

public class NumericField: CustomField {
    public var name: String;
    public var displayName: String = "Numeric Field";
    public var value: Any?;
    
    public var type: FieldType = .number;
    
    public var valueChangedCallback: CustomField.FieldOnValueChangedCallback?;
    
    public init(name: String, value: Any? = nil) {
        self.name = name;
        self.value = value;
    }
}
