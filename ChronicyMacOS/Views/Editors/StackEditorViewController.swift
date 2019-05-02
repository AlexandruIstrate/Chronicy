//
//  StackEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class StackEditorViewController: NSViewController {

    @IBOutlet private weak var titleTextField: NSTextField!;
    @IBOutlet private weak var fieldsTable: NSTableView!;
    
    @IBOutlet private weak var tableOptionsControl: NSSegmentedControl!;
    
    private lazy var menuItems: [String] = {
        return FieldType.availableTypes.map({ $0.rawValue; });
    } ();
    
    private var selectedRow: Int? {
        return self.fieldsTable.selectedRow >= 0 ? self.fieldsTable.selectedRow : nil;
    }
    
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
        case edit = 2;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupTableView();
        self.updateOptionButtons();
        
        self.titleTextField.stringValue = self.taskTitle;
    }
    
    @IBAction private func onTableOptionsClicked(_ sender: NSSegmentedControl) {
        let segmentIndex: Int = sender.selectedSegment;
        
        switch segmentIndex {
        case Segment.add.rawValue:
            onAdd();
            
        case Segment.remove.rawValue:
            onRemove();
            
        case Segment.edit.rawValue:
            onEdit();
            
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
            return cell;
            
        default:
            return nil;
        }
    }
    
    func tableViewSelectionDidChange(_ notification: Notification) {
        self.updateOptionButtons();
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
        
        self.updateOptionButtons();
    }
    
    private func onRemove() {
        guard let index: Int = self.selectedRow else {
            Log.warining(message: "Attempt to remove with no row selected");
            return;
        }
        
        self.fields.remove(at: index);
        self.reloadData();
    }
    
    private func onEdit() {
        guard let index: Int = self.selectedRow else {
            Log.error(message: "Attempt to edit with no row selected!");
            return;
        }
        
        var field: CustomField = self.fields[index];
        
        let editor: StackFieldEditorViewController = StackFieldEditorViewController();
        editor.name = field.name;
        editor.types = FieldType.availableTypes.map({ (type: FieldType) -> String in
            return type.rawValue;
        });
        editor.selectedType = field.type.rawValue;
        
        editor.callback = {
            guard let type: FieldType = FieldType(rawValue: editor.selectedType) else {
                Log.error(message: "Could not convert selected type from editor!");
                return;
            }
            
            let newField: CustomField = TextField.instantiate(by: type, name: editor.name);
            
            self.fields[index] = newField;
            self.reloadData();
        }
        
        self.present(editor, asPopoverRelativeTo: NSRect.zero, of: self.view, preferredEdge: .maxY, behavior: .transient);
    }
    
    private func updateOptionButtons() {
        // Enable if we have a selection, disable otherwise
        self.tableOptionsControl.setEnabled(self.fieldsTable.selectedRow >= 0, forSegment: Segment.remove.rawValue);
        self.tableOptionsControl.setEnabled(self.fieldsTable.selectedRow >= 0, forSegment: Segment.edit.rawValue);
    }
}
