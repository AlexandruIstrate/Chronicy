//
//  TimelineView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 08/03/2019.
//

import Cocoa;
import ChronicyFramework;

@IBDesignable
class TimelineView: NSScrollView {
    
    @IBOutlet private weak var stackView: NSStackView!;
    
    private var stackCount: Int = 0;
    private var stacks: [TimelineStackView] = [];
    
    public var dataSource: TimelineViewDataSource?;
    
}

extension TimelineView: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: TimelineViewDataSource = self.dataSource else {
            return;
        }
        
        self.stackCount = dataSource.stackCount();
        
        for i in 0..<self.stackCount {
            let stack: TimelineStackView = dataSource.stack(for: self, at: i);
            self.stacks.append(stack);
            
            stack.onLoadData();
        }
    }
    
    func onLayoutView() {
        for i in 0..<stackCount {
            let stack: TimelineStackView = stacks[i];
            self.stackView.addView(stack, in: .leading);
            
            stack.onLayoutView();
        }
    }
}

protocol TimelineViewDataSource {
    func stackCount() -> Int;
    func stack(for view: TimelineView, at index: Int) -> TimelineStackView;
}
