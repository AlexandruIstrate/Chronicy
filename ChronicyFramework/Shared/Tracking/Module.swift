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
