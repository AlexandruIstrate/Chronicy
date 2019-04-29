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
    public private(set) var stacks: [Stack] = [];
    public private(set) var activities: [Activity] = [];
    
    public init(name: String) {
        self.name = name;
    }
    
    public func add(stack: Stack) {
        self.stacks.append(stack);
    }
    
    public func add(activity: Activity) {
        self.activities.append(activity);
    }
    
    @discardableResult
    public func insertNewStack() -> Stack {
        let nameRoot: String = "New Stack";
        var name: String = nameRoot;
        var index: Int = 1;
        
        while stacks.contains(where: { (iter: Stack) -> Bool in
            iter.name == name;
        }) {
            name = "\(nameRoot) (\(index))";
            index += 1;
        }
        
        let result: Stack = Stack(name: NSLocalizedString(name, comment: ""));
        self.stacks.append(result);
        return result;
    }
    
    public func remove(stack: Stack) {
        self.stacks.removeAll { (iter: Stack) -> Bool in
            return iter == stack;
        }
    }
    
    public func remove(named: String) {
        self.stacks.removeAll { (iter: Stack) -> Bool in
            return iter.name == named;
        }
    }
    
    public func findStack(named: String) -> Stack? {
        return self.stacks.first { (iter: Stack) -> Bool in
            return iter.name == named;
        }
    }
}

extension Notebook: Equatable {
    public static func == (lhs: Notebook, rhs: Notebook) -> Bool {
        return lhs.name == rhs.name &&
            lhs.stacks == rhs.stacks &&
            lhs.activities == rhs.activities;
    }
}
