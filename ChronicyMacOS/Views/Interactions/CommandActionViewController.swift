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
    
    private static let cellIdentifier: NSUserInterfaceItemIdentifier = NSUserInterfaceItemIdentifier(rawValue: "ValueCell");
    
    enum CellOption: Int {
        case add = 0;
        case remove = 1;
        case edit = 2;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setup();
    }
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
    }
    
    override func onChangeTo() {
        guard let action: CommandAction = self.action else {
            return;
        }
        
        commandField.stringValue = action.command;
        usePrivilegesCheckbox.state = (action.useSystemPrivileges ? .on : .off);
    }
    
    override func onChangeAway() {
        self.action?.command = commandField.stringValue;
        self.action?.useSystemPrivileges = (self.usePrivilegesCheckbox.state == .on);
    }
    
    private func setup() {
        self.parametersTable.dataSource = self;
        self.parametersTable.delegate = self;
    }
    
    @IBAction private func onParameterOptionSelected(_ sender: NSSegmentedControl) {
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
        self.action?.insertNewParameter();
        self.reloadData();
    }
    
    private func onRemove() {
        guard let index: Int = self.parametersTable.selectedRowIndex else {
            return;
        }
        
        action!.params.remove(at: index);
        self.reloadData();
    }
    
    private func onEdit() {
        guard let index: Int = self.parametersTable.selectedRowIndex else {
            return;
        }
        
        let value: String = self.action!.params[index];
        
        let vc: ParameterEditViewController = ParameterEditViewController();
        vc.value = value;
        vc.callback = { (newValue: String) in
            self.action!.params[index] = newValue;
            self.reloadData();
        }
        self.present(vc, asPopoverRelativeTo: NSRect.zero, of: self.view, preferredEdge: .maxY, behavior: .semitransient);
        
        self.reloadData();
    }
    
    private func reloadData() {
        self.parametersTable.reloadData();
    }
}

extension CommandActionViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return action!.params.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let cell: NSTableCellView = tableView.makeView(withIdentifier: CommandActionViewController.cellIdentifier, owner: self) as? NSTableCellView else {
            Log.error(message: "Could not create cell for parameters table in CommandActionViewController.");
            return nil;
        }
        
        let parameter: String = self.action!.params[row];
        cell.textField?.stringValue = parameter;
        
        return cell;
    }
}
