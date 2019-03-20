//
//  MainViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class MainViewController: NSViewController {
    
    @IBOutlet private weak var sidebarView: SidebarView!;
    @IBOutlet private weak var contentView: NSView!;
    
    private var outlineView: OutlineViewController!;
    private var timeline: Timeline = Timeline(name: "Test");

    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.timeline.add(task: Task(name: "Finally fixed the UI"));
        self.timeline.add(task: Task(name: "Test"));
        
        setupContentView();
        setupTimeline();
    }
    
}

extension MainViewController: OutlineViewDataSource {
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

extension MainViewController: OutlineStackViewDataSource {
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

extension MainViewController {
    private func setupContentView() {
        self.outlineView = OutlineViewController();
        self.contentView.addSubview(self.outlineView.view);
    }
    
    private func setupTimeline() {
        self.outlineView.dataSource = self;
        self.outlineView.onLoadData();
        self.outlineView.onLayoutView();

    }
}
