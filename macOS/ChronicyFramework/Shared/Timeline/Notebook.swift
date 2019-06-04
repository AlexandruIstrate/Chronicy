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
    private var internalStacks: [Stack] = [];
    private var internalActivities: [Activity] = [];
    
    public var stacks: [Stack] {
        get {
            self.sort();
            return self.internalStacks;
        }
        set {
            self.internalStacks = newValue;
            self.sort();
        }
    }
    
    public var activities: [Activity] {
        get {
            self.sort();
            return self.internalActivities;
        }
        set {
            self.internalActivities = newValue;
            self.sort();
        }
    }
    
    public init(name: String) {
        self.name = name;
    }
    
    public func add(stack: Stack) {
        self.stacks.append(stack);
        ActivityManager.manager.add(withTitle: "Added stack \(stack.name) to notebook \(self.name)");
    }
    
    public func add(activity: Activity) {
        self.internalActivities.append(activity);
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
        self.add(stack: result);
        return result;
    }
    
    public func remove(stack: Stack) {
        self.internalStacks.removeAll { (iter: Stack) -> Bool in
            return iter == stack;
        }
        ActivityManager.manager.add(withTitle: "Removed stack \(stack.name) from notebook \(self.name)");
    }
    
    public func remove(named: String) {
        self.internalStacks.removeAll { (iter: Stack) -> Bool in
            return iter.name == named;
        }
        ActivityManager.manager.add(withTitle: "Removed stack \(named) from notebook \(self.name)");
    }
    
    public func findStack(named: String) -> Stack? {
        return self.internalStacks.first { (iter: Stack) -> Bool in
            return iter.name == named;
        }
    }
    
    private func sort() {
        self.internalStacks.sort(by: stackSort);
    }
}

extension Notebook: Equatable {
    public static func == (lhs: Notebook, rhs: Notebook) -> Bool {
        return lhs.name == rhs.name &&
            lhs.stacks == rhs.stacks &&
            lhs.activities == rhs.activities;
    }
}
