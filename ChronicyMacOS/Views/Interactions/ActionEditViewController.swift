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
    
    @IBOutlet private weak var configureArea: NSView!;
    
    @IBOutlet private weak var triggerTypePopUp: NSPopUpButton!
    @IBOutlet private weak var triggerTable: NSTableView!;
    
    private var actionViewController: NSViewController?;
    
    public var action: Action?;
    
    private static let cellIdentifier: NSUserInterfaceItemIdentifier = NSUserInterfaceItemIdentifier("TriggerNameCell");
    
    private var selectedKind: TriggerManager.Kind {
        return TriggerManager.Kind(rawValue: self.triggerTypePopUp.stringValue) ?? .url;
    }
    
    enum TableOptionButton: Int {
        case add = 0;
        case remove = 1;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.setupTable();
        self.setupTypesPopUp();
        self.loadTriggersPopUp();
        self.displayAction();
        
        self.configure(action: self.action!);
    }
    
    override func viewDidDisappear() {
        action?.name = self.nameField.stringValue;
        action?.enabled = (self.enabledCheckbox.state == .on);
        // Triggers are directly added to the Action
        
        super.viewDidDisappear();
    }
    
    private func setupTable() {
        self.triggerTable.dataSource = self;
        self.triggerTable.delegate = self;
    }
    
    private func setupTypesPopUp() {
        self.actionPopUp.removeAllItems();
        
        addTypeToPopUp(kind: Action.Kind.command);
        addTypeToPopUp(kind: Action.Kind.application);
    }
    
    private func addTypeToPopUp(kind: Action.Kind) {
        self.actionPopUp.addItem(withTitle: kind.rawValue);
    }
    
    private func loadTriggersPopUp() {
        self.triggerTypePopUp.removeAllItems();
        addTypeToTriggers(kind: .url);
    }
    
    private func addTypeToTriggers(kind: TriggerManager.Kind) {
        guard !triggerExists(action: self.action!, kind: kind) else {
            return;
        }
        
        self.triggerTypePopUp.addItem(withTitle: kind.rawValue);
    }
    
    private func triggerExists(action: Action, kind: TriggerManager.Kind) -> Bool {
        guard let trigger: ModuleTrigger = TriggerManager.manager.trigger(kind: kind) else {
            return false;
        }
        
        return action.triggers.contains(trigger);
    }
    
    private func displayAction() {
        guard let action: Action = self.action else {
            Log.error(message: "Action for edit view not set!");
            return;
        }
        
        self.nameField.stringValue = action.name;
        self.enabledCheckbox.state = (action.enabled ? .on : .off);
    }
    
    @IBAction private func onTriggerOptionsSelected(_ sender: NSSegmentedControl) {
        switch sender.selectedSegment {
        case TableOptionButton.add.rawValue:
            self.onAdd();
        case TableOptionButton.remove.rawValue:
            self.onRemove();
        default:
            break;
        }
    }
    
    @objc
    private func onAdd() {
        self.action?.add(trigger: TriggerManager.manager.trigger(kind: self.selectedKind)!);
        self.reloadData();
    }
    
    @objc
    private func onRemove() {
        guard let index: Int = self.triggerTable.selectedRowIndex else {
            return;
        }
        
        let trigger: ModuleTrigger = self.action!.triggers[index];
        self.action!.remove(trigger: trigger);
        
        self.reloadData();
    }
    
    @IBAction private func onActionTypeChanged(_ sender: NSPopUpButton) {
        guard let kind: Action.Kind = Action.Kind(rawValue: sender.stringValue) else {
            Log.error(message: "The pop up value does not match any compatible value!");
            return;
        }

        let newAction: Action = Action.instantiate(kind: kind, name: action!.name);
        newAction.triggers = action!.triggers;
        newAction.enabled = action!.enabled;

        self.action = newAction;
        self.reloadData();
    }
    
    private func configure(action: Action) {
        actionViewController = action.viewController;
        let newView: NSView = actionViewController!.view;
        self.configureArea.addSubview(newView);
        
        newView.translatesAutoresizingMaskIntoConstraints = false;
        newView.topAnchor.constraint(equalTo: self.configureArea.topAnchor).isActive = true;
        newView.bottomAnchor.constraint(equalTo: self.configureArea.bottomAnchor).isActive = true;
        newView.leadingAnchor.constraint(equalTo: self.configureArea.leadingAnchor).isActive = true;
        newView.trailingAnchor.constraint(equalTo: self.configureArea.trailingAnchor).isActive = true;
    }
    
    private func reloadData() {
        self.triggerTable.reloadData();
        self.loadTriggersPopUp();
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
