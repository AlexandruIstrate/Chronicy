//
//  OutlineViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class OutlineViewController: NSViewController {

    @IBOutlet private weak var stackView: NSStackView!;
    
    private var internalStackCount: Int = 0;
    private var stacks: [OutlineStackView] = [];
    
    public var dataSource: OutlineViewDataSource?;
    
    public func reloadData() {
        onLoadData();
        onLayoutView();
    }

}

extension OutlineViewController: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: OutlineViewDataSource = self.dataSource else {
            return;
        }
        
        self.removeAll();
        self.internalStackCount = dataSource.stackCount(for: self);
        
        for i in 0..<self.internalStackCount {
            let stack: OutlineStackView = dataSource.stack(for: self, at: i);
            stack.stackIndex = i;
            stack.parent = self;
            self.stacks.append(stack);
            
            stack.onLoadData();
        }
    }
    
    func onLayoutView() {
        for i in 0..<self.internalStackCount {
            let stack: OutlineStackView = stacks[i];
            self.stackView.addView(stack, in: .leading);
            
            stack.onLayoutView();
        }

    }
}

extension OutlineViewController {
    private func removeAll() {
        self.stacks.removeAll();
        
        for view: NSView in stackView.views {
            stackView.removeView(view);
        }
    }
}

protocol OutlineViewDataSource {
    func stackCount(for view: OutlineViewController) -> Int;
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView;
}
