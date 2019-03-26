//
//  OutlineStackView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;
import ChronicyFramework;

@IBDesignable
class OutlineStackView: NSView {

    @IBOutlet private weak var tableView: NSTableView!;
    @IBOutlet private weak var nameLabel: NSTextField!;
    
    private lazy var optionsMenu: NSMenu = {
        let menu: NSMenu = NSMenu(title: NSLocalizedString("Options", comment: ""));
        
        let add: NSMenuItem = NSMenuItem(title: NSLocalizedString("Add", comment: ""), action: #selector(onAdd), keyEquivalent: "");
        add.target = self;

        let edit: NSMenuItem = NSMenuItem(title: NSLocalizedString("Edit...", comment: ""), action: #selector(onEdit), keyEquivalent: "");
        edit.target = self;
        
        let delete: NSMenuItem = NSMenuItem(title: NSLocalizedString("Delete", comment: ""), action: #selector(onDelete), keyEquivalent: "");
        delete.target = self;
        
        menu.items = [ add, edit, delete ];
        return menu;
    } ();
    
    private var internalCellCount: Int = 0;
    private var cells: [OutlineCellView] = [];
    
    public weak var parent: OutlineViewController?;

    public var dataSource: OutlineStackViewDataSource?;
    public var delegate: OutlineStackViewDelegate?;

    public var stackIndex: Int = 0;
    public var title: String = String();
    
    public var selectionIndex: Int { return self.tableView.selectedRow; }
    
    @IBAction private func onOptionsClicked(_ sender: NSButton) {
        optionsMenu.popUp(positioning: optionsMenu.item(at: 0), at: sender.bounds.origin, in: sender);
    }

}

extension OutlineStackView: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: OutlineStackViewDataSource = self.dataSource else {
            return;
        }
        
        self.cells.removeAll();
        self.internalCellCount = dataSource.cellCount(for: self, at: self.stackIndex);
        
        for i in 0..<self.internalCellCount {
            let cell: OutlineCellView = dataSource.cell(for: self, at: i);
            cell.cellIndex = i;
            cell.parent = self;
            self.cells.append(cell);
            
            cell.onLoadData();
        }
        
        setupTable();
        self.tableView.reloadData();
    }
    
    func onLayoutView() {
        self.nameLabel.stringValue = title;
        
        for cell: OutlineCellView in cells {
            cell.onLayoutView();
        }
    }
}

extension OutlineStackView: NSTableViewDataSource, NSTableViewDelegate {
    
    private var cellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "OutlineStackViewCell"); }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
        return cells.count;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        let cell: OutlineCellView = self.cells[row];
        return cell;
    }
    
}

extension OutlineStackView {
    private func setupTable() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
    
    @objc
    private func onAdd() {
        self.delegate?.onAdd(stackView: self);
    }
    
    @objc
    private func onEdit() {
        self.delegate?.onEdit(stackView: self);
    }
    
    @objc
    private func onDelete() {
        self.delegate?.onDelete(stackView: self);
    }
}

protocol OutlineStackViewDataSource {
    func cellCount(for stack: OutlineStackView, at index: Int) -> Int;
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView;
}

protocol OutlineStackViewDelegate {
    func onAdd(stackView: OutlineStackView);
    func onEdit(stackView: OutlineStackView);
    func onDelete(stackView: OutlineStackView);
}
