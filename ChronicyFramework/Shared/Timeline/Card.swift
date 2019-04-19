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
    
    public var fields: [CustomField] = [];
        
    public struct Styling {
        var color: CGColor?;
    }
    
    public var style: Styling = Styling(color: nil);
    
    public init(title: String) {
        self.title = title;
        self.date = Date();
    }
    
}

extension Card: Equatable {
    public static func == (lhs: Card, rhs: Card) -> Bool {
        // TODO: Change
        return  lhs.title == rhs.title;
    }
}
