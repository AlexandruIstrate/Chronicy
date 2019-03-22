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
    
    private var outlineView: OutlineViewController!;
    private var timeline: Timeline = Timeline(name: "Test");

    override func viewDidLoad() {
        super.viewDidLoad();
        
        let task: Task = Task(name: "Test");
        task.add(action: Action(name: "Action"));
        
        self.timeline.add(task: task);
        
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
        return timeline.tasks.first?.actions.count ?? 0;
//        return 5;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }
        cell.background = NSColor(calibratedRed: 0.22, green: 0.43, blue: 0.66, alpha: 1);
        cell.cornerRadius = 15.0;
        
        return cell;
    }
}

extension MainViewController {
    private func setupContentView() {
        self.outlineView = OutlineViewController();
        self.view.addSubview(self.outlineView.view);
        
        let ov: NSView = self.outlineView.view;
        let cv: NSView = self.view;
        
        ov.translatesAutoresizingMaskIntoConstraints = false;
        ov.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        ov.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        ov.leadingAnchor.constraint(equalTo: sidebarView.trailingAnchor).isActive = true;
        ov.trailingAnchor.constraint(equalTo: cv.trailingAnchor).isActive = true;
    }
    
    private func setupTimeline() {
        self.outlineView.dataSource = self;
        self.outlineView.onLoadData();
        self.outlineView.onLayoutView();

    }
}
