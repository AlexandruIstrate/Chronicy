//
//  CardEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa;
import ChronicyFramework;

class CardEditorViewController: NSViewController {
    
    @IBOutlet private weak var titleField: NSTextField!;
    @IBOutlet private weak var datePicker: NSDatePicker!;
    
    @IBOutlet private weak var fieldsTable: NSTableView!;
    @IBOutlet private weak var configureView: NSView!;
    
    @IBOutlet private weak var colorWell: NSColorWell!;
    @IBOutlet private weak var tagsTable: NSTableView!;
    
    private var fieldsManager: CustomFieldManager = CustomFieldManager.manager;
    
    public var actionTitle: String = String();
    public var actionDate: Date = Date();
    public var fields: [CustomField] = [];
    
    public var actionColor: NSColor = NSColor.white { didSet { self.colorWell.color = self.actionColor; } }
    
    typealias ActionEditorCompletionHandler = (Bool) -> ();
    public var completion: ActionEditorCompletionHandler?;
    
    enum Identifier: String {
        case fieldCell = "FieldCell";
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setup();
    }
    
    override func viewWillAppear() {
        self.setupDisplayValues();
        super.viewWillAppear();
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        setupReturnValues();
        self.dismiss(nil);
        completion?(true);
    }
    
    @IBAction func onCancelPressed(_ sender: NSButton) {
        self.dismiss(nil);
        completion?(false);
    }
    
}

extension CardEditorViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return self.fieldsManager.fields.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let cell: NSTableCellView = tableView.makeView(withIdentifier: NSUserInterfaceItemIdentifier(Identifier.fieldCell.rawValue), owner: self) as? NSTableCellView else {
            Log.error(message: "Could not create cell for CardEditorViewController.");
            return nil;
        }
        
        let field: CustomField = self.fieldsManager.fields[row];
        cell.textField?.stringValue = field.name;
        
        return cell;
    }
}

extension CardEditorViewController {
    private func setup() {
        self.fieldsTable.dataSource = self;
        self.fieldsTable.delegate = self;
    }
    
    private func setupDisplayValues() {
        self.titleField.stringValue = self.actionTitle;
        self.datePicker.dateValue = self.actionDate;
    }
    
    private func setupReturnValues() {
        self.actionTitle = self.titleField.stringValue;
        self.actionDate = self.datePicker.dateValue;
    }
}

