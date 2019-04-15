//
//  StackEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa;
import ChronicyFramework;

class StackEditorViewController: NSViewController {

    @IBOutlet private weak var titleTextField: NSTextField!;
    @IBOutlet private weak var fieldsTable: NSTableView!;
    
    @IBOutlet private weak var tableOptionsControl: NSSegmentedControl!;
    
    private lazy var menuItems: [String] = {
        return FieldType.availableTypes.map({ $0.rawValue; });
    } ();
    
    public var taskTitle: String = String();
    public var fields: [CustomField] = [];
    
    typealias TaskEditorCompletionHandler = (Bool) -> ();
    public var completion: TaskEditorCompletionHandler?;
    
    enum Identifier: String {
        case nameCell = "Name";
        case typeCell = "Type";
    }
    
    enum Segment: Int {
        case add = 0;
        case remove = 1;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupTableView();
        self.updateRemoveButtonState();
        
        self.titleTextField.stringValue = self.taskTitle;
    }
    
    @IBAction private func onTableOptionsClicked(_ sender: NSSegmentedControl) {
        let segmentIndex: Int = sender.selectedSegment;
        
        switch segmentIndex {
        case Segment.add.rawValue:
            onAdd();
            
        case Segment.remove.rawValue:
            onRemove();
            
        default:
            break;
        }
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        self.taskTitle = self.titleTextField.stringValue;
        
        completion?(true);
        self.dismiss(nil);
    }
    
    @IBAction private func onCancelPressed(_ sender: NSButton) {
        completion?(false);
        self.dismiss(nil);
    }
}

extension StackEditorViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return self.fields.count;
    }
    
    func tableView(_ tableView: NSTableView, objectValueFor tableColumn: NSTableColumn?, row: Int) -> Any? {
        let field: CustomField = self.fields[row];
        
        switch tableColumn?.title {
        case Identifier.nameCell.rawValue:
            let cell: NSTextFieldCell = NSTextFieldCell(textCell: field.name);
            return cell;
            
        case Identifier.typeCell.rawValue:
            let cell: NSComboBoxCell = NSComboBoxCell(textCell: field.type.rawValue);
            cell.addItems(withObjectValues: self.menuItems);
            return cell;
            
        default:
            return nil;
        }
    }
    
    func tableViewSelectionDidChange(_ notification: Notification) {
        self.updateRemoveButtonState();
    }
}

extension StackEditorViewController {
    private func setupTableView() {
        self.fieldsTable.dataSource = self;
        self.fieldsTable.delegate = self;
    }
    
    private func reloadData() {
        self.fieldsTable.reloadData();
    }
    
    private func onAdd() {
        self.fields.append(TextField(name: NSLocalizedString("New Field", comment: "")));
        self.reloadData();
        
        self.updateRemoveButtonState();
    }
    
    private func onRemove() {
        let index: Int = self.fieldsTable.selectedRow;
        
        guard index >= 0 else {
            Log.warining(message: "Attempt to remove with no row selected");
            return;
        }
        
        self.fields.remove(at: index);
        self.reloadData();
    }
    
    private func updateRemoveButtonState() {
        // Enable if we have a selection, disable otherwise
        self.tableOptionsControl.setEnabled(self.fieldsTable.selectedRow >= 0, forSegment: Segment.remove.rawValue);
    }
}
