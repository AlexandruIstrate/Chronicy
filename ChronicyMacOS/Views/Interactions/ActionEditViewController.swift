//
//  ActionEditViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 06/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ActionEditViewController: NSViewController {
    @IBOutlet private weak var nameField: NSTextField!;
    @IBOutlet private weak var actionPopUp: NSPopUpButton!;
    
    @IBOutlet private weak var enabledCheckbox: NSButton!
    
    @IBOutlet private weak var triggerTypePupUp: NSPopUpButton!
    @IBOutlet private weak var triggerTable: NSTableView!;
    
    public var action: Action?;
    
    private static let cellIdentifier: NSUserInterfaceItemIdentifier = NSUserInterfaceItemIdentifier("TriggerNameCell");
    
    enum TableOptionButton: Int {
        case add = 0;
        case remove = 1;
    }

    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.setupTable();
        self.displayAction();
    }
    
    private func setupTable() {
        self.triggerTable.dataSource = self;
        self.triggerTable.delegate = self;
    }
    
    private func displayAction() {
        guard let action: Action = self.action else {
            Log.error(message: "Action for edit view not set!");
            return;
        }
        
        self.nameField.stringValue = action.name;
        self.enabledCheckbox.state = (action.enabled ? .on : .off);
    }
    
    @IBAction private func onOptionSelected(_ sender: NSSegmentedControl) {
        switch sender.selectedSegment {
        case TableOptionButton.add.rawValue:
            self.onAdd();
        case TableOptionButton.remove.rawValue:
            self.onRemove();
        default:
            break;
        }
    }
    
    private func onAdd() {
        self.action!.insertNewTrigger();
        self.reloadData();
    }
    
    private func onRemove() {
        guard let index: Int = self.triggerTable.selectedRowIndex else {
            return;
        }
        
        let trigger: ModuleTrigger = self.action!.triggers[index];
        self.action!.remove(trigger: trigger);
        
        self.reloadData();
    }
    
    private func reloadData() {
        self.triggerTable.reloadData();
    }
}

extension ActionEditViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return action!.triggers.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        let trigger: ModuleTrigger = self.action!.triggers[row];
        
        guard let cell: NSTableCellView = tableView.makeView(withIdentifier: ActionEditViewController.cellIdentifier, owner: self) as? NSTableCellView else {
            Log.error(message: "Could not create cell for ActionEditViewController!");
            return nil;
        }
        
        cell.textField?.stringValue = trigger.triggerName;
        return cell;
    }
}
