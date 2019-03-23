//
//  ModuleManagerViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 23/03/2019.
//

import Cocoa;
import ChronicyFramework;

class ModuleManagerViewController: NSViewController {
    
    private let moduleManager: ModuleManager = ModuleManager.manager;
    
}

extension ModuleManagerViewController: NSTableViewDataSource, NSTableViewDelegate {
    
    private var cellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "ModuleManagerViewCell"); }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
        return moduleManager.modules.count;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        let module: Module = moduleManager.modules[row];
        
        return nil;
    }
    
}
