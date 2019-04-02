//
//  OutlineCentralViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 26/03/2019.
//

import Cocoa;
import ChronicyFramework;

import CoreData;

class OutlineCentralViewController: NSViewController {

    private var outlineView: OutlineViewController!;
    private var notebook: Notebook = Notebook();
        
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupContentView();
        setupTimeline();
        setupObservers();
    }
    
    override func viewWillDisappear() {
        self.saveData();
        super.viewWillDisappear();
    }
    
}

extension OutlineCentralViewController: OutlineViewDataSource {
    func stackCount(for view: OutlineViewController) -> Int {
        return notebook.items.count;
    }
    
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView {
        guard let stack: OutlineStackView = OutlineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        let cardStack: Stack = notebook.items[index];
        
        stack.dataSource = self;
        stack.delegate = self;
        stack.title = cardStack.name;
        
        return stack;
    }
}

extension OutlineCentralViewController: OutlineStackViewDataSource, OutlineStackViewDelegate {
    func cellCount(for stack: OutlineStackView, at index: Int) -> Int {
        return notebook.items[index].cards.count;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }

        cell.background = NSColor(calibratedRed: 70 / 255.0, green: 168 / 255.0, blue: 74 / 255.0, alpha: 1);
        cell.cornerRadius = 15.0;
        cell.delegate = self;
        
//        var colors: [NSColor] = [];
//
//        var i: Float = 0.0;
//        while i < 1.0 {
//            let color: NSColor = NSColor(hue: CGFloat(i), saturation: 1.0, brightness: 0.25, alpha: 1.0);
//            colors.append(color);
//
//            i += 0.05;
//        }
        
//        cell.background = colors.randomElement() ?? NSColor(calibratedRed: 0.22, green: 0.43, blue: 0.66, alpha: 1);
        
        let card: Card = self.notebook.items[stack.stackIndex].cards[index];
        cell.title = card.title;
//        cell.subtitle = action.comment;
//        cell.date = action.date as Date;
        
        return cell;
    }
    
    func onAdd(stackView: OutlineStackView) {
        let cardStack: Stack = notebook.items[stackView.stackIndex];
//        cardStack.insertNewAction();
        
        self.reloadData();
    }
    
    func onEdit(stackView: OutlineStackView) {
        let stack: Stack = notebook.items[stackView.stackIndex];
        
        let editor: TaskEditorViewController = TaskEditorViewController();
        editor.taskTitle = stack.name;
//        editor.taskComment = task.comment;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            stack.name = editor.taskTitle;
//            task.comment = editor.taskComment;
            
            self.reloadData();
//            self.timeline.notifyChanged();
        };
        
        presentAsSheet(editor);
    }
    
    func onDelete(stackView: OutlineStackView) {
        let cardStack: Stack = notebook.items[stackView.stackIndex];
        self.notebook.remove(stack: cardStack);
        
        self.reloadData();
    }
}

extension OutlineCentralViewController: OutlineCellViewDelegate {
    func onEdit(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let card: Card = self.notebook.items[stackView.stackIndex].cards[cellView.cellIndex];
        
        let editor: ActionEditorViewController = ActionEditorViewController();
        editor.actionTitle = card.title;
//        editor.actionComment = action.comment;
//        editor.actionDate = action.date as Date;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            card.title = editor.actionTitle;
//            action.comment = editor.actionComment;
//            action.date = editor.actionDate;
            
            self.reloadData();
        };
        
        self.presentAsSheet(editor);
    }
    
    func onDelete(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let stack: Stack = self.notebook.items[stackView.stackIndex];
        let card: Card = stack.cards[cellView.cellIndex];
        
        stack.remove(card: card);
        self.reloadData();
    }
}

extension OutlineCentralViewController: ContentView {
    func reloadData() {
        self.saveData();
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
        
    @objc
    private func onApplicationAdd(notification: Notification) {
//        self.notebook.insertNewTask();
        self.reloadData();
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
    
    private func saveData() {
        do {
            guard (CoreDataStack.stack.managedObjectContext?.hasChanges ?? false) else {
                return;
            }
            
            guard ((try CoreDataStack.stack.managedObjectContext?.save()) != nil) else {
                throw NSError(domain: "CoreData", code: 99679, userInfo: nil);
            }
        } catch let e {
            self.presentError(e);
        }
    }
}
