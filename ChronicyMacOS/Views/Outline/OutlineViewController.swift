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
    
}

extension OutlineViewController: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: OutlineViewDataSource = self.dataSource else {
            return;
        }
        
        self.internalStackCount = dataSource.stackCount();
        
        for i in 0..<self.internalStackCount {
            let stack: OutlineStackView = dataSource.stack(for: self, at: i);
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

protocol OutlineViewDataSource {
    func stackCount() -> Int;
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView;
}
