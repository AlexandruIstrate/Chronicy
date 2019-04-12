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
 
    typealias FieldOnValueChangedCallback = (CustomField) -> ();
    var valueChangedCallback: FieldOnValueChangedCallback? { get set }
    
}

extension CustomField {
    public var typeName: String {
        return self.type.rawValue;
    }
    
    public func valueOfType<T>() -> T? {
        return self.value as? T;
    }
    
    public func isCorrectType(value: Any, type: inout FieldType) -> Bool {
        if value is String && type == .string {
            type = .string;
            return true;
        }
        
        if value is Int && type == .number {
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
}

public class CustomFieldManager {
    
    public static let manager: CustomFieldManager = CustomFieldManager();
    
    public private(set) var fields: [CustomField] = [];
    
    private init() {
        self.registerDefaults();
    }
    
    public func register(field: CustomField) {
        self.fields.append(field);
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

extension CustomFieldManager {
    private func registerDefaults() {
        register(field: TextField(name: "textField"));
        register(field: NumericField(name: "numericField"));
    }
}
