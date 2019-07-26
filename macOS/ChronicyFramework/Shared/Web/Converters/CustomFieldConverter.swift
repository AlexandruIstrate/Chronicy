//
//  CustomFieldConverter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class CustomFieldConverter: Converter<CustomField, CustomFieldModel> {
    public override var allowsReverseConvert: Bool { return true; }
    
    public override func convert(item: CustomField) -> CustomFieldModel {
        let result: CustomFieldModel = CustomFieldModel();
//        result.id = item.id;
        result.name = item.name;
        result.type = item.type.rawValue;
        result.value = convertFieldValueToString(fieldValue: item.value ?? "", fieldType: item.type);
        return result;
    }
    
    public override func reverseConvert(item: CustomFieldModel) -> CustomField {
        guard let type: FieldType = FieldType(rawValue: item.type.lowercased()) else { // TODO: Lowercasing is a temporary fix
            fatalError("The field type is not a supported value");
        }
        
        var result: CustomField? = nil;
        
        switch type {
        case .number:
            result = NumericField(name: item.name, value: Float(item.value ?? "0.0") ?? 0.0);
        case .string:
            result = TextField(name: item.name, value: item.value);
        }
        
//        result.id = item.id;
        
        return result!;
    }
    
    private func convertFieldValueToString(fieldValue: Any, fieldType: FieldType) -> String? {
        switch fieldType {
        case .number:
            guard let value: Float = fieldValue as? Float else {
                return nil;
            }
            return String(value);
        case .string:
            return fieldValue as? String;
        }
    }
    
    private func convertStringToFieldValue(fieldValue: String, fieldType: FieldType) -> Any? {
        switch fieldType {
        case .number:
            return Float(fieldValue);
        case .string:
            return fieldValue;
        }
    }
}
