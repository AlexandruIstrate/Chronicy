//
//  ModuleManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public class ModuleManager {
    
    public static let manager: ModuleManager = ModuleManager();
    
    private var modules: [Module] = [];
    private let updateQueue: DispatchQueue = DispatchQueue(label: "ModuleManagerUpdateQueue", qos: .background);
    
    private init() {
        let timer: Timer = Timer(fireAt: Date(), interval: 5, target: self, selector: #selector(onUpdate), userInfo: nil, repeats: true);
        RunLoop.main.add(timer, forMode: .common);
    }
    
    public func add(module: Module) {
        updateQueue.async(flags: .barrier) {
            self.modules.append(module);
        }
    }
    
    public func remove(module: Module) {
        updateQueue.async(flags: .barrier) {
            self.modules.removeAll { (iter: Module) -> Bool in
                return iter.moduleName() == module.moduleName();
            }
        }
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
