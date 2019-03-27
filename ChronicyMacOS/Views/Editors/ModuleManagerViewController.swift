//
//  ModuleManagerViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 23/03/2019.
//

import Cocoa;
import ChronicyFramework;

class ModuleManagerViewController: NSViewController {
    
    @IBOutlet private weak var tableView: NSTableView!;
    
    private let moduleManager: ModuleManager = ModuleManager.manager;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        setup();
    }
    
}

extension ModuleManagerViewController: NSTableViewDataSource, NSTableViewDelegate {
    
    enum CellIdentifier: String {
        case nameCell = "NameCell";
        case stateCell = "StateCell";
    }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
        return moduleManager.modules.count;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        var cellIdentifier: CellIdentifier!;
        
        switch tableColumn {
        case tableView.tableColumns[0]:
            cellIdentifier = .nameCell;
            break;
            
        case tableView.tableColumns[1]:
            cellIdentifier = .stateCell;
            break;
            
        default:
            Log.error(message: "Could not find required cell for ModuleManagerViewController's NSTableView.")
            return nil;
        }
        
        guard let cell: NSTableCellView = tableView.makeView(withIdentifier: NSUserInterfaceItemIdentifier(cellIdentifier.rawValue), owner: self) as? NSTableCellView else {
            Log.error(message: "Could not create cell for ModuleManagerViewController, with identifier \(cellIdentifier.rawValue)")
            return nil;
        }
        
        let module: Module = moduleManager.modules[row];
        cell.textField?.stringValue = module.moduleName();
        
        return cell;
    }
    
}

extension ModuleManagerViewController {
    private func setup() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
}
