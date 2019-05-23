//
//  Action.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 05/05/2019.
//

import Foundation;

public class Action: Equatable {
    public var name: String;
    public private(set) var triggers: [ModuleTrigger] = [];
    public var enabled: Bool = false;
    
    public let id: String = UUID().uuidString;
    
    public var kind: Kind {
        switch self {
        case is CommandAction:
            return .command;
        case is ApplicationAction:
            return .application;
        default:
            fatalError("Kind does not exist!");
        }
    }
    
    public var triggerAction: ModuleTrigger.TriggerAction? {
        willSet { self.removeTriggerActions(); }
        didSet { self.updateTriggerActions(); }
    }
    
    public enum Kind: String {
        case command = "Command";
        case application = "Application";
    }
    
    public init(name: String) {
        self.name = name;
        triggerAction = ModuleTrigger.TriggerAction(trigger: { (t: ModuleTrigger) in
            if self.enabled {
                self.onTrigger();
            }
        })
    }
    
    deinit {
        self.removeTriggerActions();
    }
    
    public static func == (lhs: Action, rhs: Action) -> Bool {
        return lhs.name == rhs.name &&
            lhs.triggers == rhs.triggers &&
            lhs.enabled == rhs.enabled &&
            lhs.id == rhs.id;
    }
    
    public func add(trigger: ModuleTrigger) {
        self.triggers.append(trigger);
        
        if let action: ModuleTrigger.TriggerAction = self.triggerAction {
            trigger.register(action: action);
        }
    }
    
    public func add(triggers: [ModuleTrigger]) {
        for iter: ModuleTrigger in triggers {
            self.add(trigger: iter);
        }
    }
    
    public func remove(trigger: ModuleTrigger) {
        if let action: ModuleTrigger.TriggerAction = self.triggerAction {
            trigger.deregister(action: action);
        }
        
        self.triggers.removeAll { (iter: ModuleTrigger) -> Bool in
            return iter == trigger;
        }
    }
    
    public func onTrigger() {
        ActivityManager.manager.add(withTitle: "Triggered the action named \"\(self.name)\"");
    }
    
    public static func instantiate(kind: Kind, name: String) -> Action {
        switch kind {
        case .command:
            return CommandAction(name: name);
        case .application:
            return ApplicationAction(name: name);
        }
    }
    
    private func removeTriggerActions() {
        guard let action: ModuleTrigger.TriggerAction = self.triggerAction else {
            return;
        }
        
        for iter: ModuleTrigger in triggers {
            iter.deregister(action: action);
        }
    }
    
    private func updateTriggerActions() {
        guard let action: ModuleTrigger.TriggerAction = self.triggerAction else {
            return;
        }
        
        for iter: ModuleTrigger in triggers {
            iter.register(action: action);
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
    
    public func replace(action: Action, with: Action) {
        guard let index: Int = self.actions.firstIndex(where: { (iter: Action) -> Bool in
            return iter.id == action.id;
        }) else {
            Log.warining(message: "Action with id \(action.id) not found!");
            return;
        }
        
        self.actions[index] = with;
    }
}
