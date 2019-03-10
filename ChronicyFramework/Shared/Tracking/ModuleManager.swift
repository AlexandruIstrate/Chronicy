//
//  ModuleManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public class ModuleManager {
    
    private var modules: [Module] = [];
    
    public func add(module: Module) {
        self.modules.append(module);
    }
    
    public func remove(module: Module) {
        self.modules.removeAll { (iter: Module) -> Bool in
            return iter.moduleName() == module.moduleName();
        }
    }
}
