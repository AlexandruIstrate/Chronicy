//
//  CardTag.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 11/04/2019.
//

import Foundation;

public struct CardTag: Equatable {
    public var title: String;
    public var color: CGColor;
    
    public init(title: String, color: CGColor) {
        self.title = title;
        self.color = color;
    }
}

public class CardTagManager {
    
    public static var manager: CardTagManager = CardTagManager();
    
    public private(set) var tags: [CardTag] = [];
    
    private init() {
        self.setupDefaults();
    }
    
    public func register(tag: CardTag) {
        self.tags.append(tag);
    }
    
    public func deregister(tag: CardTag) {
        self.tags.removeAll { (iter: CardTag) -> Bool in
            return iter == tag;
        }
    }
    
    public func tags(matchingColor: CGColor) -> [CardTag] {
        return self.tags.filter({ (tag: CardTag) -> Bool in
            return tag.color == matchingColor;
        });
    }
}

extension CardTagManager {
    private func setupDefaults() {
        register(tag: CardTag(title: "Tag 1", color: CGColor(red: 1.0, green: 0.0, blue: 0.0, alpha: 1.0)));
    }
}
