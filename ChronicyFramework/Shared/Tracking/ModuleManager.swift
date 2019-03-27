//
//  ModuleManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public class ModuleManager {
    
    public static let manager: ModuleManager = ModuleManager();
    
    private var internalModules: [Module] = [];
    private let updateQueue: DispatchQueue = DispatchQueue(label: "ModuleManagerUpdateQueue", qos: .background);
    
    public var modules: [Module] {
        var result: [Module] = [];
        
        updateQueue.sync {
            result = self.internalModules;
        }
        
        return result;
    }
    
    private init() {
        let timer: Timer = Timer(fireAt: Date(), interval: 5, target: self, selector: #selector(onUpdate), userInfo: nil, repeats: true);
        RunLoop.main.add(timer, forMode: .common);
    }
    
    deinit {
        updateQueue.async(flags: .barrier) {
            for module: Module in self.internalModules {
                module.onUnload();
            }
        }
    }
    
    public func add(module: Module) {
        updateQueue.async(flags: .barrier) {
            self.internalModules.append(module);
            module.onLoad();
        }
        
        InteractionsManager.manager.reload();
    }
    
    public func remove(module: Module) {
        updateQueue.async(flags: .barrier) {
            self.internalModules.removeAll { (iter: Module) -> Bool in
                return iter.moduleName() == module.moduleName();
            }
            module.onUnload();
        }
        
        InteractionsManager.manager.reload();
    }
}

extension ModuleManager {
    @objc
    private func onUpdate() {
        for module: Module in modules {
            module.update();
        }
    }
}
