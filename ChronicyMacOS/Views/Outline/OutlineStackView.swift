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

//    @IBOutlet private weak var stackView: NSStackView!;
    @IBOutlet private weak var tableView: NSTableView!;
    
    private var cellCount: Int = 0;
    private var cells: [OutlineCellView] = [];

    public var dataSource: OutlineStackViewDataSource?;

}

extension OutlineStackView: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: OutlineStackViewDataSource = self.dataSource else {
            return;
        }
        
        self.cellCount = dataSource.cellCount();
        
        for i in 0..<self.cellCount {
            let cell: OutlineCellView = dataSource.cell(for: self, at: i);
            self.cells.append(cell);
            
            cell.onLoadData();
        }
        
        setupTable();
        self.tableView.reloadData();
    }
    
    func onLayoutView() {
//        for i in 0..<cellCount {
//            let cell: OutlineCellView = cells[i];
//            self.stackView.addView(cell, in: .top);
//
//            cell.onLayoutView();
//        }
    }
}

extension OutlineStackView: NSTableViewDataSource, NSTableViewDelegate {
    
    private var cellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "OutlineStackViewCell"); }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
        return cells.count;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.error(message: "Could not load from NIB!");
            return nil;
        }
        cell.identifier = self.cellIdentifier;
        
        return cell;
    }
    
}

extension OutlineStackView {
    private func setupTable() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
}

protocol OutlineStackViewDataSource {
    func cellCount() -> Int;
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView;
}
