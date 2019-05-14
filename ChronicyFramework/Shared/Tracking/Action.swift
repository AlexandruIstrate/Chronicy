//
//  Action.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 05/05/2019.
//

import Foundation;

public class Action: Equatable {
    public var name: String;
    public var triggers: [ModuleTrigger] = [];
    public var enabled: Bool = false;
    
    public enum Kind: String {
        case command = "Command";
        case application = "Application";
    }
    
    public init(name: String) {
        self.name = name;
    }
    
    public static func == (lhs: Action, rhs: Action) -> Bool {
        return lhs.name == rhs.name &&
            lhs.triggers == rhs.triggers &&
            lhs.enabled == rhs.enabled;
    }
    
    public func add(trigger: ModuleTrigger) {
        self.triggers.append(trigger);
    }
    
    public func remove(trigger: ModuleTrigger) {
        self.triggers.removeAll { (iter: ModuleTrigger) -> Bool in
            return iter == trigger;
        }
    }
    
    public func onTrigger() {
        
    }
    
    public static func instantiate(kind: Kind, name: String) -> Action {
        switch kind {
        case .command:
            return CommandAction(name: name);
        case .application:
            return ApplicationAction(name: name);
        }
    }
}

public class ActionManager {
    public static var manager: ActionManager = ActionManager();
    public private(set) var actions: [Action] = [];
    
    public init() {
        
    }
    
    public func add(action: Action) {
        self.actions.append(action);
    }
    
    @discardableResult
    public func insertNewAction() -> Action {
        let nameRoot: String = "New Action";
        var name: String = nameRoot;
        var index: Int = 1;
        
        while actions.contains(where: { (iter: Action) -> Bool in
            iter.name == name;
        }) {
            name = "\(nameRoot) (\(index))";
            index += 1;
        }
        
        let action: Action = CommandAction(name: NSLocalizedString(name, comment: ""));
        self.actions.append(action);
        return action;
    }
    
    public func remove(action: Action) {
        self.actions.removeAll { (iter: Action) -> Bool in
            return iter == action;
        }
    }
}
