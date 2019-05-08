//
//  InteractionsViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 05/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class InteractionsViewController: NSViewController {

    @IBOutlet private weak var leftView: NSView!;
    @IBOutlet private weak var tableView: NSTableView!;
    
    private var lastActionView: NSView?;
    
    private var actionManager: ActionManager = ActionManager();
    
    enum Identifier: String {
        case nameCell = "NameCell";
        case triggerCell = "TriggerCell"
        case enabledCell = "EnabledCell"
        
        var identifier: NSUserInterfaceItemIdentifier {
            return NSUserInterfaceItemIdentifier(rawValue: self.rawValue);
        }
    }
    
    enum ColumnName: String {
        case name = "Name";
        case trigger = "Trigger";
        case enabled = "Enabled";
    }
    
    enum TableOptionButton: Int {
        case add = 0;
        case remove = 1;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        setupTable();
    }
    
    private func setupTable() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
    
    private func displayEditView(action: Action) {
        if let view: NSView = self.lastActionView {
            view.removeFromSuperview();
        }
        
        let vc: ActionEditViewController = ActionEditViewController();
        vc.action = action;
        
        let newView: NSView = vc.view;
        self.view.addSubview(newView);
        
        newView.translatesAutoresizingMaskIntoConstraints = false;
        newView.topAnchor.constraint(equalTo: self.view.topAnchor).isActive = true;
        newView.bottomAnchor.constraint(equalTo: self.view.bottomAnchor).isActive = true;
        newView.leadingAnchor.constraint(equalTo: leftView.trailingAnchor).isActive = true;
        newView.trailingAnchor.constraint(equalTo: self.view.trailingAnchor).isActive = true;
        
        self.lastActionView = newView;
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
        self.actionManager.insertNewAction();
        self.reloadData();
    }
    
    private func onRemove() {
        guard let index: Int = self.tableView.selectedRowIndex else {
            return;
        }
        
        let action: Action = self.actionManager.actions[index];
        self.actionManager.remove(action: action);
        
        self.reloadData();
    }
    
    private func reloadData() {
        self.tableView.reloadData();
    }
}

extension InteractionsViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return self.actionManager.actions.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        let action: Action = self.actionManager.actions[row];
        
        switch tableColumn?.title {
        case ColumnName.name.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: Identifier.nameCell.identifier, owner: self) as? NSTableCellView else {
                Log.error(message: "Could not create tableview cell for InteractionsViewController!");
                return nil;
            }
            view.textField?.stringValue = action.name;
            return view;
            
        case ColumnName.trigger.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: Identifier.triggerCell.identifier, owner: self) as? NSTableCellView else {
                Log.error(message: "Could not create tableview cell for InteractionsViewController!");
                return nil;
            }
            view.textField?.stringValue = action.triggers.map({ (iter: ModuleTrigger) -> String in
                // TODO: Other more logical name
                return iter.moduleName;
            }).joined(separator: "; ");
            return view;
            
        case ColumnName.enabled.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: Identifier.triggerCell.identifier, owner: self) as? NSTableCellView else {
                Log.error(message: "Could not create tableview cell for InteractionsViewController!");
                return nil;
            }
            view.textField?.stringValue = String(action.enabled);
            return view;
            
        default:
            Log.error(message: "Could not create tableview cell for InteractionsViewController!");
        }
        
        return nil;
    }
    
    func tableViewSelectionDidChange(_ notification: Notification) {
        guard let index: Int = self.tableView.selectedRowIndex else {
            return;
        }
        
        let action: Action = self.actionManager.actions[index];
        self.displayEditView(action: action);
    }
}
