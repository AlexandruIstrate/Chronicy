//
//  OutlineStackView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

@IBDesignable
class OutlineStackView: NSView {

    @IBOutlet private weak var tableView: NSTableView!;
    @IBOutlet private weak var nameLabel: NSTextField!;
    
    private var internalCellCount: Int = 0;
    private var cells: [OutlineCellView] = [];
    
    public weak var parent: OutlineViewController?;

    public var dataSource: OutlineStackViewDataSource?;
    public var delegate: OutlineStackViewDelegate?;
    public var interactionDelegate: ViewInteractionDelegate?;
    
    public var editTrigger: OutlineViewController.ActionTrigger? = .click;
    public var deleteTrigger: OutlineViewController.ActionTrigger?;

    public var stackIndex: Int = 0;
    public var title: String = String();
    
    public var selectionIndex: Int { return self.tableView.selectedRow; }
    
    override func mouseDown(with event: NSEvent) {
        super.mouseUp(with: event);
        self.interactionDelegate?.onClick(at: event.locationInWindow, in: self);
        
        if self.editTrigger == .click {
            self.onEdit();
        }
    }
    
    override func rightMouseDown(with event: NSEvent) {
        super.rightMouseUp(with: event);
        self.interactionDelegate?.onRightClick(at: event.locationInWindow, in: self);
        
        if self.editTrigger == .rightClick {
            self.onEdit();
        }
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
        self.setupFonts();
        
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
    
    private func setupFonts() {
        let titleFont: NSFont? = NSFont(name: "JosefinSans-Regular", size: 32.0);
        self.nameLabel.font = titleFont;
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
