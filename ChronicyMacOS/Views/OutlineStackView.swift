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

    @IBOutlet private weak var stackView: NSStackView!;
    
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
    }
    
    func onLayoutView() {
        for i in 0..<cellCount {
            let cell: OutlineCellView = cells[i];
            self.stackView.addView(cell, in: .top);
            
            cell.onLayoutView();
        }
    }
}

protocol OutlineStackViewDataSource {
    func cellCount() -> Int;
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView;
}
