//
//  Notebook.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public protocol NotebookItem {
    func onAdd(to notebook: Notebook);
    func onRemove(from notebook: Notebook);
}

public class Notebook {
    public var name: String;
    public private(set) var items: [Stack] = [];
    public private(set) var activities: [Activity] = [];
    
    public init(name: String) {
        self.name = name;
    }
    
    public func add(stack: Stack) {
        self.items.append(stack);
    }
    
    @discardableResult
    public func insertNewStack() -> Stack {
        let result: Stack = Stack(name: NSLocalizedString("New Stack", comment: ""));
        self.items.append(result);
        return result;
    }
    
    public func remove(stack: Stack) {
        self.items.removeAll { (iter: Stack) -> Bool in
            return iter == stack;
        }
    }
    
    public func add(activity: Activity) {
        self.activities.append(activity);
    }
}

extension Notebook: Equatable {
    public static func == (lhs: Notebook, rhs: Notebook) -> Bool {
        return lhs.name == rhs.name &&
            lhs.items == rhs.items &&
            lhs.activities == rhs.activities;
    }
}
