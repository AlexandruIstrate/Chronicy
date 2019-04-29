//
//  FieldDefinition.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 28/04/2019.
//

import Foundation;

func compare(lhs: FieldType, rhs: FieldType) -> Bool {
    return lhs.rawValue < rhs.rawValue;
}

public struct FieldDefinition: Equatable {
    public var types: [FieldType];
    
    public func matches(other: FieldDefinition) -> Bool {
        return self.types.sorted(by: compare(lhs:rhs:)) == other.types.sorted(by: compare(lhs:rhs:));
    }
}

extension FieldContainer {
    public var definition: FieldDefinition {
        return FieldDefinition(types: self.fields.map({ (iter: CustomField) -> FieldType in
            return iter.type;
        }));
    }
}

public class FieldDefinitionManager {
    public private(set) var definitions: [FieldDefinition] = [];
    
    public func register(definition: FieldDefinition) {
        self.definitions.append(definition);
    }
    
    public func deregister(definition: FieldDefinition) {
        self.definitions.removeAll { (iter: FieldDefinition) -> Bool in
            return iter == definition;
        }
    }
    
    public func matching(container: FieldContainer) -> [FieldDefinition] {
        
        return self.definitions.filter { (definition: FieldDefinition) -> Bool in
            return container.fields.map({ (field: CustomField) -> FieldType in
                return field.type;
            }).sorted(by: compare(lhs:rhs:))
            == definition.types.sorted(by: compare(lhs:rhs:))
        }
    }
    
    public func hasMatching(container: FieldContainer) -> Bool {
        return !self.matching(container: container).isEmpty;
    }
}

public class CustomDefinitions {
    public static let url: FieldDefinition = FieldDefinition(types: [.string]);
}
