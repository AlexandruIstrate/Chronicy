//
//  Module.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

open class Module {
    
    public var properties: [TrackableProperty] = [];
    
    public init() {
        
    }
    
    open func moduleName() -> String {
        fatalError("Method moduleName is abstract and must be implemented in a subclass of this class.");
    }
    
    open func priority() -> ModulePriority {
        return .low;
    }
    
    open func triggers() -> [ModuleTrigger] {
        return [];
    }
    
    open func onLoad() {
        
    }
    
    open func onUnload() {
        
    }
    
    open func update() {
        for property: TrackableProperty in properties {
            property.onRefreshData();
        }
    }
}

public enum ModulePriority {
    case low;
    case medium;
    case high;
}

open class ModuleTrigger: Equatable {
    public var triggerName: String;
    public var moduleName: String;
    
    public struct TriggerAction: Equatable {
        public typealias TriggerFunction = (ModuleTrigger) -> ();
        public var trigger: TriggerFunction?;
        public let id: String = UUID().uuidString;
        
        public init(trigger: @escaping TriggerFunction) {
            self.trigger = trigger;
        }
        
        public static func == (lhs: ModuleTrigger.TriggerAction, rhs: ModuleTrigger.TriggerAction) -> Bool {
            return lhs.id == rhs.id;
        }
    }
    
    private var actions: [TriggerAction] = [];
    
    public init(triggerName: String, moduleName: String) {
        self.triggerName = triggerName;
        self.moduleName = moduleName;
    }
    
    public required init() {
        self.triggerName = String();
        self.moduleName = String();
    }
    
    public func register(action: TriggerAction) {
        self.actions.append(action);
    }
    
    public func deregister(action: TriggerAction) {
        self.actions.removeAll { (iter: ModuleTrigger.TriggerAction) -> Bool in
            return iter == action;
        }
    }
    
    public static func == (lhs: ModuleTrigger, rhs: ModuleTrigger) -> Bool {
        return lhs.triggerName == rhs.triggerName && lhs.moduleName == rhs.moduleName;
    }

    public func trigger() {
        for action: TriggerAction in self.actions {
            action.trigger?(self);
        }
    }
}
