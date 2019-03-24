//
//  Task.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

public class Task {
    public var name: String;
    public var comment: String;
    public let date: Date;
    
    public private(set) var actions: [Action] = [];
    
    public init(name: String, comment: String = "", date: Date = Date()) {
        self.name = name;
        self.comment = comment;
        self.date = date;
    }
    
    public func add(action: Action) {
        actions.append(action);
    }
    
    @discardableResult
    public func insertNewAction() -> Action {
        let action: Action = Action(name: "New Action");
        self.actions.append(action);
        
        return action;
    }
    
    public func remove(action: Action) {
        actions.removeAll { (iter: Action) -> Bool in
            return iter == action;
        }
    }
    
    public func has(action: Action) -> Bool {
        return actions.contains(action);
    }
}

extension Task: Equatable {
    public static func == (lhs: Task, rhs: Task) -> Bool {
        return lhs.name     == rhs.name &&
               lhs.comment  == rhs.comment &&
               lhs.date     == rhs.date &&
               lhs.actions  == rhs.actions;
    }
}

extension Task: TimeExpressible {
    public typealias T = [Action];
    
    public func older(than date: Date) -> T {
        return actions.filter({ (action: Action) -> Bool in
            return action.date < date;
        });
    }
    
    public func newer(than date: Date) -> T {
        return actions.filter({ (action: Action) -> Bool in
            return action.date > date;
        });
    }
}
