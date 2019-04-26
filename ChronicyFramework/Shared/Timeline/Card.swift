//
//  Card.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public class Card {
    public var name: String;
    public var date: Date;
    
    public var fields: [CustomField] = [];
    public var tags: [CardTag] = [];
    
    public init(title: String) {
        self.name = title;
        self.date = Date();
    }
    
    public func insertIntoFields(values: [Any?]) throws {
        guard fields.count == values.count else {
            throw InsertionError.wrongElementCount;
        }
        
        for i in 0..<self.fields.count {
            let field: CustomField = self.fields[i];
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

extension Card: Equatable {
    public static func == (lhs: Card, rhs: Card) -> Bool {
        // TODO: Change
        return  lhs.name == rhs.name;
    }
}

public enum InsertionError: Error {
    case wrongTypes;
    case wrongElementCount;
}
