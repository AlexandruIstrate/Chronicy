//
//  TriggerManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 12/05/2019.
//

import Foundation;

public class TriggerManager {
    public enum Kind: String {
        case url;
        
        public static let available: [Kind] = [.url]
    }
    
    public static let manager: TriggerManager = TriggerManager();
    
    private var triggers: [Kind : ModuleTrigger] = [:];
    
    public init() {
        
    }
    
    public func trigger(kind: Kind) -> ModuleTrigger? {
        return triggers[kind];
    }
    
    public subscript(kind: Kind) -> ModuleTrigger? {
        return self.trigger(kind: kind);
    }
    
    public func register(register: TriggerManagerRegister) {
        for item in register.triggers() {
            triggers[item.key] = item.value;
        }
    }
    
    public func raise(kind: Kind) {
        self.triggers[kind]?.trigger();
    }
}

public protocol TriggerManagerRegister {
    func triggers() -> [TriggerManager.Kind : ModuleTrigger];
}
