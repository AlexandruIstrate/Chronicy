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
    
    open func triggers() -> [InteractionsManager.Interactable] {
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

public protocol KeyInstance {
    static var instance: AnyObject { get }
}

open class ModuleTrigger: Equatable {
    
    public var triggerName: String;
    public var moduleName: String;
    
    public typealias TriggerAction = (ModuleTrigger) -> ();
    public var action: TriggerAction?;
    
    public init(triggerName: String, moduleName: String) {
        self.triggerName = triggerName;
        self.moduleName = moduleName;
    }
    
    public required init() {
        self.triggerName = String();
        self.moduleName = String();
    }
    
    public static func == (lhs: ModuleTrigger, rhs: ModuleTrigger) -> Bool {
        return lhs.triggerName == rhs.triggerName && lhs.moduleName == rhs.moduleName;
    }

    public func trigger() {
        self.action?(self);
    }
}
