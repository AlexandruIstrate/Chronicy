//
//  InteractionsManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/03/2019.
//

import Foundation;

public class InteractionsManager {
    public static let manager: InteractionsManager = InteractionsManager();
    public private(set) var triggers: [ModuleTrigger] = [];
    
    private init() {
        self.reload();
    }
    
    public func register(trigger: ModuleTrigger, action: @escaping ModuleTrigger.TriggerAction) {
        trigger.action = action;
        self.triggers.append(trigger);
    }
    
    public func raise(trigger: ModuleTrigger) {
        self.triggers.first { (iter: ModuleTrigger) -> Bool in
            return iter == trigger;
        }?.action?(trigger);
    }
    
    public func reload() {
        self.triggers.removeAll();

        for module: Module in ModuleManager.manager.modules {
            triggers.append(contentsOf: module.triggers());
        }
    }
}
