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
            self.addSubview(cell);
            
            cell.onLayoutView();
        }
    }
}

protocol TimelineStackViewDataSource {
    func cellCount() -> Int;
    func cell(for stack: TimelineStackView, at index: Int) -> TimelineCellView;
}

//@IBDesignable
//class TimelineStackView: NSView {
//
//    private var cellCount: Int = 0;
//    private var cells: [TimelineCellView] = [];
//
//    public var dataSource: TimelineStackViewDataSource? { didSet { reloadData(); } }
//
//    public func reloadData() {
//        guard let dataSource: TimelineStackViewDataSource = self.dataSource else {
//            return;
//        }
//
//        self.cellCount = dataSource.cellCount();
//
//        for i in 0..<self.cellCount {
//            self.cells.append(dataSource.cell(at: i));
//        }
//
//        layoutCells();
//    }
//}
//
//extension TimelineStackView {
//    private func layoutCells() {
//        for _ in 0..<cellCount {
//            guard let view: TimelineCellView = TimelineCellView.fromNib() else {
//                Log.error(message: "Could not load TimelineCellView!");
//                return;
//            }
//
//            self.addSubview(view);
//        }
//    }
//
//    private func computeCellHeight(cell: TimelineCellView) -> CGFloat {
//        // TODO: Change so that it includes the amount of text in the cell
//        return self.frame.height / CGFloat(integerLiteral: self.cellCount);
//    }
//}
//
//protocol TimelineStackViewDataSource {
//    func cellCount() -> Int;
//    func cell(at index: Int) -> TimelineCellView;
//}
