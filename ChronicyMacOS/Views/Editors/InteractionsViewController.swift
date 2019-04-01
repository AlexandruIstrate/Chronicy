//
//  InteractionsViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 26/03/2019.
//

import Cocoa;
import ChronicyFramework;

class InteractionsViewController: NSViewController {

    @IBOutlet private weak var tableView: NSTableView!;
    @IBOutlet private weak var editButton: NSButton!;
    @IBOutlet private weak var clearButton: NSButton!;
    
    private let interactions: InteractionsManager = InteractionsManager.manager;
    
    private lazy var textLabelFont: NSFont? = {
        return NSFont(name: "JosefinSans-Regular", size: 13.0);
    } ();
    
    override func viewDidLoad() {
        super.viewDidLoad();
        setup();
    }
    
    @IBAction private func onEdit(_ sender: NSButton) {
        let vc: InteractionsEditViewController = InteractionsEditViewController();
        vc.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
        
            let index: Int = self.tableView.selectedRow;
            
            let interaction: InteractionsManager.Interactable = self.interactions.triggers[index];
            interaction.action = vc.action;
            
            self.tableView.reloadData();
        };
        
        self.presentAsSheet(vc);
    }
    
    @IBAction private func onClear(_ sender: NSButton) {
        let index: Int = self.tableView.selectedRow;
        
        let interaction: InteractionsManager.Interactable = self.interactions.triggers[index];
        interaction.action = nil;
        
        self.tableView.reloadData();
    }
}

extension InteractionsViewController: NSTableViewDataSource, NSTableViewDelegate {
    
    enum CellIdentifier: String {
        case moduleNameCell = "ModuleNameCell";
        case triggerCell = "TriggerCell";
        case actionCell = "ActionCell";
    }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
        return interactions.triggers.count;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        var cellIdentifier: CellIdentifier = .moduleNameCell;
        
        switch tableColumn {
        case tableView.tableColumns[0]:
            cellIdentifier = .moduleNameCell;
            break;
            
        case tableView.tableColumns[1]:
            cellIdentifier = .triggerCell;
            break;
            
        case tableView.tableColumns[2]:
            cellIdentifier = .actionCell;
            break;
            
        default:
            Log.error(message: "Could not find required cell for InteractionsViewController NSTableView.")
            return nil;
        }
        
        guard let cell: NSTableCellView = tableView.makeCell(identifier: cellIdentifier.rawValue) else {
            Log.error(message: "Could not create cell for InteractionsViewController, with identifier \(cellIdentifier.rawValue)")
            return nil;
        }
        
        cell.textField?.font = self.textLabelFont;
        
        let interactable: InteractionsManager.Interactable = self.interactions.triggers[row];
        
        switch cellIdentifier {
        case .moduleNameCell:
            cell.textField?.stringValue = interactable.moduleName;
            
        case .triggerCell:
            cell.textField?.stringValue = interactable.triggerName;
            
        case .actionCell:
            cell.textField?.stringValue = (interactable.action == nil ? "Not Set" : "Will Trigger Action");
        }
        
        return cell;
    }
    
    func tableViewSelectionDidChange(_ notification: Notification) {
        self.onSelectionChanged();
    }
    
}

extension InteractionsViewController {
    private func setup() {
        self.setupView();
        
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
    
    private func setupView() {
        self.onDeselectedRow();
    }
    
    private func onSelectionChanged() {
        let index: Int = tableView.selectedRow;
        
        guard index != -1 else {
            self.onDeselectedRow();
            return;
        }
        
        self.onSelectedRow(index: index);
    }
    
    private func onSelectedRow(index: Int) {
        self.editButton.isEnabled = true;
        self.clearButton.isEnabled = true;
    }
    
    private func onDeselectedRow() {
        self.editButton.isEnabled = false;
        self.clearButton.isEnabled = false;
    }
}

