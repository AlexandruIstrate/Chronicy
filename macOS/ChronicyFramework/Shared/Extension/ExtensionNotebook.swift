//
//  ExtensionNotebook.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/04/2019.
//

import Foundation;

public struct ExtensionNotebook: Equatable, Codable {
    public var name: String;
    public var stacks: [ExtensionStack];
    
    public init(name: String, stacks: [ExtensionStack] = []) {
        self.name = name;
        self.stacks = stacks;
    }
    
    public func named(name: String) -> ExtensionStack? {
        return self.stacks.filter({ (stack: ExtensionStack) -> Bool in
            return stack.name == name;
        }).first;
    }
}
