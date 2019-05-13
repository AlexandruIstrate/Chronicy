//
//  CommandActionViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class CommandActionViewController: ActionViewController<CommandAction> {

    @IBOutlet private weak var commandField: NSTextField!;
    @IBOutlet private weak var usePrivilegesCheckbox: NSButton!;
    
    @IBOutlet private weak var parametersTable: NSTableView!;
    
    private var params: [String : String] = [:];
    
    enum ColumnName: String {
        case name = "Name";
        case value = "Value";
        
        var identifier: NSUserInterfaceItemIdentifier {
            return NSUserInterfaceItemIdentifier(rawValue: self.rawValue);
        }
    }
    
    enum CellOption: Int {
        case add = 0;
        case remove = 1;
        case edit = 2;
    }
    
    override func onChangeTo() {
        guard let action: CommandAction = self.action else {
            return;
        }
        
        commandField.stringValue = "\(action.name) \(self.composeParameters())";
        usePrivilegesCheckbox.state = (action.useSystemPrivileges ? .on : .off);
    }
    
    override func onChangeAway() {
        self.action?.command = commandField.stringValue;
        self.action?.useSystemPrivileges = (self.usePrivilegesCheckbox.state == .on);
    }
    
    @IBAction private func onOptionSelected(_ sender: NSSegmentedControl) {
        switch sender.selectedSegment {
        case CellOption.add.rawValue:
            onAdd();
        case CellOption.remove.rawValue:
            onRemove();
        case CellOption.edit.rawValue:
            onEdit();
        default:
            break;
        }
    }
    
    private func onAdd() {
        let nameRoot: String = "param";
        var name: String = nameRoot;
        var index: Int = 1;
        
        while self.params.keys.contains(where: { (iter: String) -> Bool in
            return iter == name;
        }) {
            name = nameRoot + String(index);
            index += 1;
        }
        
        self.params[name] = "";
    }
    
    private func onRemove() {
        guard let index: Int = self.parametersTable.selectedRowIndex else {
            return;
        }
        
        let keys: [String] = Array(self.params.keys);
        let key: String = keys[index];
        
        self.params.removeValue(forKey: key);
    }
    
    private func onEdit() {
        guard let index: Int = self.parametersTable.selectedRowIndex else {
            return;
        }
        
        
    }
    
    private func composeParameters() -> String {
        return "";
    }
}

extension CommandActionViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return params.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        return nil;
    }
}
