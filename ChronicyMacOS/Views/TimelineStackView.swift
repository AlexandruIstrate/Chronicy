//
//  TimelineStackView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;
import ChronicyFramework;

@IBDesignable
class TimelineStackView: NSView {

    @IBOutlet private weak var stackView: NSStackView!;
    
    private var cellCount: Int = 0;
    private var cells: [TimelineCellView] = [];

    public var dataSource: TimelineStackViewDataSource?;

}

extension TimelineStackView: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: TimelineStackViewDataSource = self.dataSource else {
            return;
        }
        
        self.cellCount = dataSource.cellCount();
        
        for i in 0..<self.cellCount {
            let cell: TimelineCellView = dataSource.cell(for: self, at: i);
            self.cells.append(cell);
            
            cell.onLoadData();
        }
    }
    
    func onLayoutView() {
        for i in 0..<cellCount {
            let cell: TimelineCellView = cells[i];
            self.stackView.addView(cell, in: .top);
            
            cell.onLayoutView();
        }
    }
}

protocol TimelineStackViewDataSource {
    func cellCount() -> Int;
    func cell(for stack: TimelineStackView, at index: Int) -> TimelineCellView;
}
