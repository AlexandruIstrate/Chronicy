//
//  ViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFramework;

class ViewController: NSViewController, NSTextViewDelegate {
    
    private var timelineView: TimelineView!;
    
    private var timeline: Timeline = Timeline(name: "Test");

    override var representedObject: Any? {
        didSet {
            // Pass down the represented object to all of the child view controllers.
            for child: NSViewController in children {
                child.representedObject = representedObject;
            }
        }
    }

    weak var document: Document? {
        if let docRepresentedObject: Document = representedObject as? Document {
            return docRepresentedObject;
        }
        
        return nil;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.loadMainView();
        
        ModuleManager.manager.add(module: SafariBrowserModule());
        
        timeline.add(task: Task(name: "Task 1"));
        timeline.add(task: Task(name: "Task 2"));
        timeline.add(task: Task(name: "Task 3"));
        timeline.tasks.first!.add(action: Action(name: "Test", date: Date()));
        
        self.timelineView.dataSource = self;
        
        self.timelineView.onLoadData();
        self.timelineView.onLayoutView();
    }

    // MARK: - NSTextViewDelegate

    func textDidBeginEditing(_ notification: Notification) {
        document?.objectDidBeginEditing(self);
    }

    func textDidEndEditing(_ notification: Notification) {
        document?.objectDidEndEditing(self);
    }

}

extension ViewController: TimelineViewDataSource {
    func stackCount() -> Int {
//        return timeline.tasks.count;
        return 3;
    }

    func stack(for view: TimelineView, at index: Int) -> TimelineStackView {
        guard let stack: TimelineStackView = TimelineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        stack.dataSource = self;
        return stack;
    }
}

extension ViewController: TimelineStackViewDataSource {
    func cellCount() -> Int {
//        return timeline.tasks.first!.actions.count;
        return 3;
    }

    func cell(for stack: TimelineStackView, at index: Int) -> TimelineCellView {
        guard let cell: TimelineCellView = TimelineCellView.fromNib() else {
            Log.fatal(message: "Could not create TimelineCellView.");
            fatalError();
        }
        
        return cell;
    }
}

extension ViewController {
    private func loadMainView() {
        self.timelineView = TimelineView.fromNib();
//        self.timelineView.translatesAutoresizingMaskIntoConstraints = false;
//        
//        self.timelineView.topAnchor.constraint(equalTo: self.view.topAnchor);
//        self.timelineView.bottomAnchor.constraint(equalTo: self.view.bottomAnchor);
//        self.timelineView.leadingAnchor.constraint(equalTo: self.view.leadingAnchor);
//        self.timelineView.trailingAnchor.constraint(equalTo: self.view.trailingAnchor);
        
        self.view.addSubview(timelineView);
    }
}
