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
            self.addSubview(stack);
            
            stack.onLayoutView();
        }
    }
}

protocol TimelineViewDataSource {
    func stackCount() -> Int;
    func stack(for view: TimelineView, at index: Int) -> TimelineStackView;
}

//@IBDesignable
//class TimelineView: NSScrollView {
//
//    private var stackCount: Int = 0;
//    private var stacks: [TimelineStackView] = [];
//
//    public var dataSource: TimelineViewDataSource? { didSet { reloadData(); } }
//
//    public func reloadData() {
//        guard let dataSource: TimelineViewDataSource = self.dataSource else {
//            return;
//        }
//
//        self.stackCount = dataSource.stackCount();
//
//        for i in 0..<self.stackCount {
//            let stack: TimelineStackView = dataSource.stack(at: i);
//            self.stacks.append(stack);
//        }
//
//        layoutStacks();
//    }
//
//}
//
//extension TimelineView {
//    private func layoutStacks() {
//        for _ in 0..<stackCount {
//            guard let view: TimelineStackView = TimelineStackView.fromNib() else {
//                Log.error(message: "Could not load TimelineStackView!");
//                return;
//            }
//
//            self.addSubview(view);
//        }
//    }
//}
//
//protocol TimelineViewDataSource {
//    func stackCount() -> Int;
//    func stack(at index: Int) -> TimelineStackView;
//}
