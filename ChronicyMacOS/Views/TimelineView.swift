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
    
    public var dataSource: TimelineViewDataSource? { didSet { reloadData(); } }
    
    public func reloadData() {
        guard let dataSource: TimelineViewDataSource = self.dataSource else {
            return;
        }
        
        self.stackCount = dataSource.stackCount();
        
        for i in 0..<self.stackCount {
            self.stacks.append(dataSource.stack(at: i));
        }
        
        layoutStacks();
    }

}

extension TimelineView {
    private func layoutStacks() {
        for _ in 0..<stackCount {
            guard let view: TimelineStackView = TimelineStackView.fromNib() else {
                Log.error(message: "Could not load TimelineStackView!");
                return;
            }
            
            self.addSubview(view);
        }
    }
}

protocol TimelineViewDataSource {
    func stackCount() -> Int;
    func stack(at index: Int) -> TimelineStackView;
}
