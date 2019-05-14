//
//  FieldContainer.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/04/2019.
//

import Foundation;

public protocol FieldContainer {
    var fields: [ExtensionField] { get set }
}

extension ExtensionField {
    public func isCorrectType(value: Any, type: inout FieldType) -> Bool {
        if value is String && type == .string {
            type = .string;
            return true;
        }
        
        if value is Float && type == .number {
            type = .number;
            return true;
        }
        
        return false;
    }
}

extension FieldContainer {
    public func insertIntoFields(values: [Any?]) throws {
        guard fields.count == values.count else {
            throw InsertionError.wrongElementCount;
        }
        
        for i in 0..<self.fields.count {
            let field: ExtensionField = self.fields[i];
            let value: Any? = values[i];
            
            var type: FieldType = FieldType.string;
            
            guard value != nil else {
                continue;
            }
            
            guard field.isCorrectType(value: value!, type: &type) else {
                throw InsertionError.wrongTypes;
            }
        }
    }
}

public enum InsertionError: Error {
    case wrongElementCount;
    case wrongTypes;
}
