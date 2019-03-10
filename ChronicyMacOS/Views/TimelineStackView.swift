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
    
    private var cellCount: Int = 0;
    private var cells: [TimelineCellView] = [];
    
    public var dataSource: TimelineStackViewDataSource? { didSet { reloadData(); } }
    public var delegate: TimelineStackViewDelegate?;
    
    public func reloadData() {
        guard let dataSource: TimelineStackViewDataSource = self.dataSource else {
            return;
        }
        
        self.cellCount = dataSource.cellCount();
        
        for i in 0..<self.cellCount {
            self.cells.append(dataSource.cell(at: i));
        }
        
        layoutCells();
    }
}

extension TimelineStackView {
    private func layoutCells() {
        for _ in 0..<cellCount {
            guard let view: TimelineCellView = TimelineCellView.fromNib() else {
                Log.error(message: "Could not load TimelineCellView!");
                return;
            }
            
            self.addSubview(view);
        }
    }
}

protocol TimelineStackViewDataSource {
    func cellCount() -> Int;
    func cell(at index: Int) -> TimelineCellView;
}

protocol TimelineStackViewDelegate {
    func cellHeight() -> Int;
}
