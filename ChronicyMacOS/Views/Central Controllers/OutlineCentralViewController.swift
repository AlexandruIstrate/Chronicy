//
//  OutlineCentralViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 26/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

import CoreData;

class OutlineCentralViewController: NSViewController {

    private var outlineView: OutlineViewController!;
    
    private let notebookManager: NotebookManager = LocalNotebookManager();
    private var notebook: Notebook = Notebook(name: "Main");
    
    private var notebookNames: [String] = [];
    
    private let broadcaster: InformationBroadcaster = InformationBroadcaster();
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let card1: Card = Card(title: "Card 1");
        let card2: Card = Card(title: "Card 2");
        let card3: Card = Card(title: "Card 3");
        
        let stack1: Stack = Stack(name: "Stack");
        stack1.add(card: card1);
        stack1.add(card: card2);
        stack1.add(card: card3);
        
        let otherCard1: Card = Card(title: "Card 1");
        let otherCard2: Card = Card(title: "Card 2");
        
        let stack2: Stack = Stack(name: "Stack");
        stack2.add(card: otherCard1);
        stack2.add(card: otherCard2);
        
        self.notebook.add(stack: stack1);
        self.notebook.add(stack: stack2);
        
//        displayLoadingView();
        loadNotebookData();
        setupContentView();
        setupTimeline();
        setupToolbarCallbacks();
    }
    
    override func viewWillDisappear() {
        self.saveData();
        super.viewWillDisappear();
    }
    
}

extension OutlineCentralViewController: OutlineViewDataSource {
    func stackCount(for view: OutlineViewController) -> Int {
        return notebook.stacks.count;
    }
    
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView {
        guard let stack: OutlineStackView = OutlineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        let cardStack: Stack = notebook.stacks[index];
        
        stack.dataSource = self;
        stack.delegate = self;
        stack.title = cardStack.name;
        
        return stack;
    }
}

extension OutlineCentralViewController: OutlineStackViewDataSource, OutlineStackViewDelegate {
    func cellCount(for stack: OutlineStackView, at index: Int) -> Int {
        return notebook.stacks[index].cards.count;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }

        cell.editTrigger = .click;
        cell.delegate = self;
        
        let card: Card = self.notebook.stacks[stack.stackIndex].cards[index];
        cell.title = card.name;
        cell.date = card.date;
        
        return cell;
    }
    
    func onAdd(stackView: OutlineStackView) {
        let cardStack: Stack = notebook.stacks[stackView.stackIndex];
        cardStack.insertNewCard();
        
        self.reloadData();
    }
    
    func onEdit(stackView: OutlineStackView) {
        let stack: Stack = notebook.stacks[stackView.stackIndex];
        
        let editor: StackEditorViewController = StackEditorViewController();
        editor.taskTitle = stack.name;
        editor.fields = stack.inputTemplate.fields;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            stack.name = editor.taskTitle;
            stack.inputTemplate.fields = editor.fields;
            
            self.reloadData();
//            self.timeline.notifyChanged();
        };
        
        presentAsSheet(editor);
    }
    
    func onDelete(stackView: OutlineStackView) {
        let cardStack: Stack = notebook.stacks[stackView.stackIndex];
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
        
        let stack: Stack = self.notebook.stacks[stackView.stackIndex];
        let card: Card = stack.cards[cellView.cellIndex];
        
        let editor: CardEditorViewController = CardEditorViewController();
        editor.actionTitle = card.name;
        editor.actionDate = card.date;
        editor.fields = stack.inputTemplate.fields;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            card.name = editor.actionTitle;
            card.date = editor.actionDate;
            card.fields = editor.fields;
            
            self.reloadData();
        };
        
        self.presentAsSheet(editor);
    }
    
    func onDelete(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let stack: Stack = self.notebook.stacks[stackView.stackIndex];
        let card: Card = stack.cards[cellView.cellIndex];
        
        stack.remove(card: card);
        self.reloadData();
    }

}

extension OutlineCentralViewController: ContentView {
    func reloadData() {
        self.saveData();
        self.outlineView.reloadData();
        
        broadcaster.broadcastNotebooks(notebooks: [notebook]);
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
    
    private func setupToolbarCallbacks() {
        WindowController.shared.delegate = self;
    }
    
    private func loadNotebookData() {
        self.notebookManager.retrieveNotebook(info: NotebookInfo(name: "Test", id: "Test", dateCreated: Date())) { (notebook: Notebook?, error: NotebookManagerError?) in
            guard let notebook: Notebook = notebook else {
                self.presentError(error!);
                return;
            }
            
            self.notebook = notebook;
        }

        self.notebookManager.getInfo { (info: [NotebookInfo]?, error: NotebookManagerError?) in
            guard let info: [NotebookInfo] = info else {
                self.presentError(error!);
                return;
            }
            
            self.notebookNames = info.map({ (iter: NotebookInfo) -> String in
                return iter.name;
            })
        }
    }
    
    private func displayLoadingView() {
        let alert: NSAlert = NSAlert();
        alert.messageText = "Test";
        alert.runModal();
    }
    
    private func saveData() {
//        do {
//            guard (CoreDataStack.stack.managedObjectContext?.hasChanges ?? false) else {
//                return;
//            }
//            
//            guard ((try CoreDataStack.stack.managedObjectContext?.save()) != nil) else {
//                throw NSError(domain: "CoreData", code: 99679, userInfo: nil);
//            }
//        } catch let e {
//            self.presentError(e);
//        }
    }
}

extension OutlineCentralViewController: WindowControllerDataSource, WindowControllerDelegate {
    func notebooks() -> [String] {
        return self.notebookNames;
    }
    
    func onAdd() {
        self.notebook.insertNewStack();
        self.reloadData();
    }
    
    func onRemove() {
        let controller: StackEditViewController = StackEditViewController();
        controller.stacks = self.notebook.stacks.map({ (iter: Stack) -> String in
            return iter.name;
        })
        controller.callback = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            for name: String in controller.removedStacks {
                self.notebook.remove(named: name);
            }
            
            self.reloadData();
        }
        
        self.presentAsSheet(controller);
    }
    
    func onEdit() {
        
    }
    
    func onNotebookChanged(oldName: String?, newName: String) {
        
    }
    
    func onSearch(term: String) {
        
    }
    
    func enabledItems() -> [Bool] {
        return [];
    }
}
