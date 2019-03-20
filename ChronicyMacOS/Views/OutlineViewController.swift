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
    
    public var timeline: Timeline = Timeline(name: "Test");
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.timeline.add(task: Task(name: "Test"))
        
        self.dataSource = self;
        
        self.onLoadData();
        self.onLayoutView();
    }
    
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

extension OutlineViewController: OutlineViewDataSource {
    func stackCount() -> Int {
        return timeline.tasks.count;
    }
    
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView {
        guard let stack: OutlineStackView = OutlineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        stack.dataSource = self;
        return stack;
    }
}

extension OutlineViewController: OutlineStackViewDataSource {
    func cellCount() -> Int {
//        return timeline.tasks.first?.actions.count ?? 0;
        return 5;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }
        
        return cell;
    }
}


protocol OutlineViewDataSource {
    func stackCount() -> Int;
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView;
}
