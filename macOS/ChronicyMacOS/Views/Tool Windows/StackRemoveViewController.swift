//
//  StackEditViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 26/04/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class StackRemoveViewController: NSViewController {
    
    @IBOutlet private weak var tableView: NSTableView!;
    @IBOutlet private weak var optionsControl: NSSegmentedControl!;
    
    public var stacks: [String] = [];
    public private(set) var removedStacks: [String] = [];
    
    typealias StackEditViewControllerCallback = (Bool) -> ();
    public var callback: StackEditViewControllerCallback?;
    
    public var selectedRow: Int? {
        let index: Int = self.tableView.selectedRow;
        
        guard index >= 0 else {
            return nil;
        }
        
        return index;
    }
    
    private static let cellIdentifier: NSUserInterfaceItemIdentifier = NSUserInterfaceItemIdentifier("Cell");
    
    enum ButtonIndex: Int {
        case remove = 0;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupDelegate();
    }
    
    private func setupDelegate() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
    
    @IBAction private func onOptionBarClicked(_ sender: NSSegmentedControl) {
        let index: Int = sender.indexOfSelectedItem;
        
        if index == ButtonIndex.remove.rawValue {
            self.onRemove();
        }
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        self.removedStacks = self.stacks;
        self.callback?(true);
        self.dismiss(nil);
    }
    
    @IBAction private func onCancelPressed(_ sender: NSButton) {
        self.callback?(false);
        self.dismiss(nil);
    }
    
    private func onRemove() {
        guard let index: Int = self.selectedRow else {
            return;
        }
        
        self.stacks.remove(at: index);
        self.reloadData();
    }
    
    private func reloadData() {
        self.tableView.reloadData();
    }
}

extension StackRemoveViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return stacks.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let cell: NSTableCellView = tableView.makeView(withIdentifier: StackRemoveViewController.cellIdentifier, owner: self) as? NSTableCellView else {
            Log.error(message: "Could not create cell for StackEditViewController table view!");
            return nil;
        }
        
        let stack: String = self.stacks[row];
        
        cell.textField?.stringValue = stack;
        return cell;
    }
}
