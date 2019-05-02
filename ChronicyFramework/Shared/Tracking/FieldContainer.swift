//
//  FieldContainer.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/04/2019.
//

import Foundation;

public protocol FieldContainer {
    var fields: [CustomField] { get set }
}

extension FieldContainer {
    public mutating func insertIntoFields(values: [Any?]) throws {
        guard fields.count == values.count else {
            throw InsertionError.wrongElementCount;
        }
        
        for i in 0..<self.fields.count {
            var field: CustomField = self.fields[i];
            let value: Any? = values[i];
            
            var type: FieldType = FieldType.string;
            
            guard value != nil else {
                continue;
            }
            
            guard field.isCorrectType(value: value!, type: &type) else {
                throw InsertionError.wrongTypes;
            }
            
            field.value = value;
            self.fields[i] = field;
            
            print("Field");
        }
        
        for field: CustomField in self.fields {
            Log.info(message: "\(field.name) : \(field.value)");
        }
    }
}

public enum InsertionError: Error {
    case wrongElementCount;
    case wrongTypes;
}
