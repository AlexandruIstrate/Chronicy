//
//  Card.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public class Card {
    
    public var title: String;
    public var date: Date;
    
    public private(set) var fields: [CustomField] = [];
    
    public init(title: String) {
        self.title = title;
        self.date = Date();
    }
}

extension Card: Equatable {
    public static func == (lhs: Card, rhs: Card) -> Bool {
        return false;
    }
}
