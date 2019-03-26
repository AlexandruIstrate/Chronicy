//
//  OutlineCentralViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 26/03/2019.
//

import Cocoa;
import ChronicyFramework;

class OutlineCentralViewController: NSViewController {

    private var outlineView: OutlineViewController!;
    private var timeline: Timeline = TimelineManager.manager.timeline;
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupContentView();
        setupTimeline();
        
        setupObservers();
        registerTaskObservers();
        
        setupModules();
    }
    
}

extension OutlineCentralViewController: OutlineViewDataSource {
    func stackCount(for view: OutlineViewController) -> Int {
        return timeline.tasks.count;
    }
    
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView {
        guard let stack: OutlineStackView = OutlineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        let task: Task = timeline.tasks[index];
        
        stack.dataSource = self;
        stack.delegate = self;
        stack.title = task.name;
        
        return stack;
    }
}

extension OutlineCentralViewController: OutlineStackViewDataSource, OutlineStackViewDelegate {
    func cellCount(for stack: OutlineStackView, at index: Int) -> Int {
        return timeline.tasks[index].actions.count;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }
        
        cell.background = NSColor(calibratedRed: 0.22, green: 0.43, blue: 0.66, alpha: 1);
        cell.cornerRadius = 15.0;
        cell.delegate = self;
        
        let action: Action = self.timeline.tasks[stack.stackIndex].actions[index];
        cell.title = action.name;
        cell.date = action.date;
        
        return cell;
    }
    
    func onAdd(stackView: OutlineStackView) {
        let task: Task = timeline.tasks[stackView.stackIndex];
        task.insertNewAction();
        
        self.outlineView.reloadData();
    }
    
    func onEdit(stackView: OutlineStackView) {
        let task: Task = timeline.tasks[stackView.stackIndex];
        
        let editor: TaskEditorViewController = TaskEditorViewController();
        editor.taskTitle = task.name;
        editor.taskComment = task.comment;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            task.name = editor.taskTitle;
            task.comment = editor.taskComment;
            
            self.outlineView.reloadData();
        };
        
        presentAsSheet(editor);
    }
    
    func onDelete(stackView: OutlineStackView) {
        let task: Task = timeline.tasks[stackView.stackIndex];
        self.timeline.remove(task: task);
        
        self.outlineView.reloadData();
    }
}

extension OutlineCentralViewController: OutlineCellViewDelegate {
    func onEdit(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let action: Action = self.timeline.tasks[stackView.stackIndex].actions[cellView.cellIndex];
        
        let editor: ActionEditorViewController = ActionEditorViewController();
        editor.actionTitle = action.name;
        editor.actionComment = action.comment;
        editor.actionDate = action.date;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            action.name = editor.actionTitle;
            action.comment = editor.actionComment;
            action.date = editor.actionDate;
            
            self.outlineView.reloadData();
        };
        
        self.presentAsSheet(editor);
    }
    
    func onDelete(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let task: Task = self.timeline.tasks[stackView.stackIndex];
        let action: Action = task.actions[cellView.cellIndex];
        
        task.remove(action: action);
        self.outlineView.reloadData();
    }
}

extension OutlineCentralViewController {
    private func setupContentView() {
        self.outlineView = OutlineViewController();
        self.view.addSubview(self.outlineView.view);
        
        let ov: NSView = self.outlineView.view;
        let cv: NSView = self.view;
        
        ov.translatesAutoresizingMaskIntoConstraints = false;
        ov.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        ov.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        ov.leadingAnchor.constraint(equalTo: cv.leadingAnchor).isActive = true;
        ov.trailingAnchor.constraint(equalTo: cv.trailingAnchor).isActive = true;
    }
    
    private func setupTimeline() {
        self.outlineView.dataSource = self;
        self.outlineView.onLoadData();
        self.outlineView.onLayoutView();
    }
    
    private func setupObservers() {
        NotificationCenter.default.addObserver(self, selector: #selector(onApplicationAdd), name: WindowController.Notifications.toolbarAdd.name, object: nil);
        NotificationCenter.default.addObserver(self, selector: #selector(onApplicationRemove), name: WindowController.Notifications.toolbarRemove.name, object: nil);
        NotificationCenter.default.addObserver(self, selector: #selector(onApplicationEdit), name: WindowController.Notifications.toolbarEdit.name, object: nil);
    }
    
    private func setupModules() {
        ModuleManager.manager.add(module: SafariBrowserModule());
    }
    
    @objc
    private func onApplicationAdd(notification: Notification) {
        self.timeline.insertNewTask();
        self.outlineView.reloadData();
    }
    
    @objc
    private func onApplicationRemove(notification: Notification) {
//        if let selectedStackIndex: Int = outlineView.selectedStackIndex  {
//
//        } else
        
//        if let selectedActionIndex: Int = outlineView.selectedActionIndex  {
//            
//        }
    }
    
    @objc
    private func onApplicationEdit(notification: Notification) {
        Log.info(message: "onEdit");
    }
}

extension OutlineCentralViewController {
    private func registerTaskObservers() {
        let nc: NotificationCenter = NotificationCenter.default;
        nc.addObserver(self, selector: #selector(onTasksModified), name: NSNotification.Name(rawValue: Timeline.Notifications.tasksModified.rawValue), object: nil);
    }
    
    private func deregisterTaskObservers() {
        let nc: NotificationCenter = NotificationCenter.default;
        nc.removeObserver(self, name: NSNotification.Name(rawValue: Timeline.Notifications.tasksModified.rawValue), object: nil);
    }
    
    @objc
    private func onTasksModified(notification: Notification) {
        let taskNames: [String] = self.timeline.tasks.map { (task: Task) -> String in
            return task.name;
        }
        DistributedObjectManager.manager.set(object: taskNames, for: SharedConstants.DistributedObjectKeys.tasks);
    }
}

