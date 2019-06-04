//
//  Card.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public class Card: FieldContainer {
    public var name: String;
    public var date: Date;
    
    public var fields: [CustomField] = [];
    public var tags: [CardTag] = [];
    
    public init(title: String, date: Date = Date()) {
        self.name = title;
        self.date = date;
    }
    
}

extension Card: Equatable {
    public static func == (lhs: Card, rhs: Card) -> Bool {
        // TODO: Change
        return  lhs.name == rhs.name &&
            lhs.date == rhs.date;
    }
}
