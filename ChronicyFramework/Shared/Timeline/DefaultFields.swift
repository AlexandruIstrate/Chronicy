//
//  DefaultFields.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 08/04/2019.
//

import Foundation;

public class TextField: CustomField {
    public var displayName: String = "Text Field";
    public var value: Any?;
    
    public var type: FieldType = .string;
    
    public var valueChangedCallback: CustomField.FieldOnValueChangedCallback?;
}

public class NumericField: CustomField {
    public var displayName: String = "Numeric Field";
    public var value: Any?;
    
    public var type: FieldType = .number
    
    public var valueChangedCallback: CustomField.FieldOnValueChangedCallback?;
}
