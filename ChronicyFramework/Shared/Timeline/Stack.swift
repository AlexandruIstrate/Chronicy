//
//  Stack.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//
//

import Foundation;

public class Stack {
    
    public var name: String;
    public private(set) var cards: [Card] = [];
    
    public struct Styling {
        var color: CGColor;
    }
    
    public var style: Styling = Styling(color: CGColor.white);
    
    public init(name: String) {
        self.name = name;
    }

    public func add(card: Card) {
        self.cards.append(card);
    }
    
    public func remove(card: Card) {
        self.cards.removeAll { (iter: Card) -> Bool in
            return iter == card;
        }
    }
    
    @discardableResult
    public func insertNewCard() -> Card {
        let card: Card = Card(title: NSLocalizedString("Test", comment: ""));
        self.cards.append(card);
        return card;
    }

}

extension Stack: NotebookItem {
    public func onAdd(to notebook: Notebook) {
        
    }
    
    public func onRemove(from notebook: Notebook) {
        
    }
}

extension Stack: Equatable {
    public static func == (lhs: Stack, rhs: Stack) -> Bool {
        return lhs.name    == rhs.name &&
               lhs.cards   == rhs.cards;
    }
}
