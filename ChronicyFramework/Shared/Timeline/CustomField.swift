//
//  CustomField.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public protocol CustomField {
    var name: String { get set }
    var displayName: String { get }
    var value: Any? { get set }
    
    var type: FieldType { get }
}

extension CustomField {
    public func valueOfType<T>() -> T? {
        return self.value as? T;
    }
    
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

public enum FieldType: String {
    case string;
    case number;
    // TODO: Add more
    
    public static var availableTypes: [FieldType] = {
        return [.string, .number];
    } ();
}

public class CustomFieldInputTemplate {
    
    public var fields: [CustomField] = [];

    public init() {
        // No-Op
    }
    
    public func register(field: CustomField) {
        self.fields.append(field);
    }
    
    public func register(fields: [CustomField]) {
        self.fields.append(contentsOf: fields);
    }
    
    public func deregister(field: CustomField) {
        self.fields.removeAll { (iter: CustomField) -> Bool in
            return iter.type == field.type;
        }
    }
    
    @discardableResult
    public func deregister(named: String) -> CustomField? {
        let field: CustomField? = self.fields.first { (iter: CustomField) -> Bool in
            return iter.name == named;
        }
        
        guard let result: CustomField = field else {
            return nil;
        }
        
        self.deregister(field: result);
        return result;
    }
}
