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
    private var internalCards: [Card] = [];
    
    public private(set) var inputTemplate: CustomFieldInputTemplate = CustomFieldInputTemplate();
    
    public var cards: [Card] {
        get {
            self.sort();
            return self.internalCards;
        }
        set {
            self.internalCards = newValue;
            self.sort();
        }
    }
    
    public init(name: String) {
        self.name = name;
    }

    public func add(card: Card) {
        self.internalCards.append(card);
        ActivityManager.manager.add(withTitle: "Added card \(card.name) to stack \(self.name)");
    }
    
    public func remove(card: Card) {
        self.internalCards.removeAll { (iter: Card) -> Bool in
            return iter == card;
        }
        ActivityManager.manager.add(withTitle: "Remove card \(card.name) from stack \(self.name)");
    }
    
    @discardableResult
    public func insertNewCard() -> Card {
        let nameRoot: String = "New Card";
        var name: String = nameRoot;
        var index: Int = 1;
        
        while internalCards.contains(where: { (iter: Card) -> Bool in
            iter.name == name;
        }) {
            name = "\(nameRoot) (\(index))";
            index += 1;
        }
        
        let card: Card = Card(title: NSLocalizedString(name, comment: ""));
        card.fields = self.inputTemplate.fields;
        self.add(card: card);
        return card;
    }
    
    private func sort() {
        self.internalCards.sort(by: cardSort);
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

public enum DataInsertionError: Error {
    case invalidDataType;
    case invalidFieldCount;
}
