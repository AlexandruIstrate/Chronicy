//
//  CardEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa;
import ChronicyFramework;

class CardEditorViewController: NSViewController {

    @IBOutlet private weak var titleLabel: NSTextField!;
    @IBOutlet private weak var dateLabel: NSTextField!;
    
    @IBOutlet private weak var availableOutline: NSOutlineView!;
    @IBOutlet private weak var activeTable: NSScroller!;
    
    public var actionTitle: String = String();
    public var actionDate: Date = Date();
    
    public var actionColor: NSColor = NSColor.white;
    
    public private(set) var availableFields: [EditorField] = [];
    public private(set) var activeFields: [EditorField] = [];
    
    typealias ActionEditorCompletionHandler = (Bool) -> ();
    public var completion: ActionEditorCompletionHandler?;
    
    enum Identifier: String {
        case available;
        case active;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        
        self.dismiss(nil);
        completion?(true);
    }
    
    @IBAction func onCancelPressed(_ sender: NSButton) {
        self.dismiss(nil);
        completion?(false);
    }
    
}

extension CardEditorViewController {
    private func loadAvailable() {
        
    }
    
    private func cellForField(field: EditorField, tableView: NSTableView, identifier: Identifier) -> NSTableCellView? {
        guard let cell: NSTableCellView = tableView.makeCell(identifier: identifier.rawValue) else {
            Log.error(message: "Could not create cell for CardEditorViewController with identifier \(identifier.rawValue)");
            return nil;
        }
        
        return cell;
    }
}

struct EditorField {
    var name: String;
    var value: Any?;    // Should this be here?
}

class FieldsTableDataSource: NSObject, NSTableViewDataSource, NSTableViewDelegate {
    
    public var fields: [EditorField] = [];
    
//    init(fields: [) {
//
//    }
    
    func numberOfRows(in tableView: NSTableView) -> Int {
        return self.fields.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        return nil;
    }
}
