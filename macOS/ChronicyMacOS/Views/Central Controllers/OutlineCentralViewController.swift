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
    
    private var notebookManager: NotebookManager = LocalNotebookManager();
    private var notebook: Notebook?;
    
    private var notebookNames: [NotebookInfo] = [];
    
    private let broadcaster: InformationBroadcaster = InformationBroadcaster();
    
    override func viewDidLoad() {
        super.viewDidLoad();
        notebookManager = NotebookManagerFactory.create(type: DataSourceManager.manager.sourceType);
        
//        displayLoadingView();
        setupContentView();
        setupTimeline();
        setupToolbarCallbacks();
        
        DataSourceManager.manager.subscribe(item: self);
        
        loadNotebookData();
        broadcastNotebooks();
    }
    
    override func viewWillDisappear() {
        self.saveData();
        super.viewWillDisappear();
    }
    
    public func notifyFieldsInserted() {
        loadNotebookData();
    }
    
}

extension OutlineCentralViewController: OutlineViewDataSource {
    func stackCount(for view: OutlineViewController) -> Int {
        guard let notebook: Notebook = self.notebook else {
            return 0;
        }
        
        return notebook.stacks.count;
    }
    
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView {
        guard let stack: OutlineStackView = OutlineStackView.fromNib() else {
            Log.fatal(message: "Could not create TimelineStackView.");
            fatalError();
        }
        
        let cardStack: Stack = notebook!.stacks[index];
        
        stack.dataSource = self;
        stack.delegate = self;
        stack.title = cardStack.name;
        
        return stack;
    }
}

extension OutlineCentralViewController: OutlineStackViewDataSource, OutlineStackViewDelegate {
    func cellCount(for stack: OutlineStackView, at index: Int) -> Int {
        return notebook!.stacks[index].cards.count;
    }
    
    func cell(for stack: OutlineStackView, at index: Int) -> OutlineCellView {
        guard let cell: OutlineCellView = OutlineCellView.fromNib() else {
            Log.fatal(message: "Could not create OutlineCellView.");
            fatalError();
        }

        cell.editTrigger = .click;
        cell.delegate = self;
        
        let card: Card = self.notebook!.stacks[stack.stackIndex].cards[index];
        cell.title = card.name;
        cell.subtitle = card.comment;
        cell.date = card.date;
        
        return cell;
    }
    
    func onAdd(stackView: OutlineStackView) {
        let cardStack: Stack = notebook!.stacks[stackView.stackIndex];
        cardStack.insertNewCard();
        
        self.reloadData();
    }
    
    func onEdit(stackView: OutlineStackView) {
        let stack: Stack = notebook!.stacks[stackView.stackIndex];
        
        let editor: StackEditorViewController = StackEditorViewController();
        editor.taskTitle = stack.name;
        editor.fields = stack.inputTemplate.fields;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            stack.name = editor.taskTitle;
//            stack.inputTemplate.fields = editor.fields;
            stack.inputTemplate.register(replace: editor.fields);
            
            self.reloadData();
//            self.timeline.notifyChanged();
        };
        
        presentAsSheet(editor);
    }
    
    func onDelete(stackView: OutlineStackView) {
        let cardStack: Stack = notebook!.stacks[stackView.stackIndex];
        self.notebook!.remove(stack: cardStack);
        
        self.reloadData();
    }
}

extension OutlineCentralViewController: OutlineCellViewDelegate {
    func onEdit(cellView: OutlineCellView) {
        guard let stackView: OutlineStackView = cellView.parent else {
            Log.error(message: "Could not find OutlineStackView for OutlineCellView!")
            return;
        }
        
        let stack: Stack = self.notebook!.stacks[stackView.stackIndex];
        let card: Card = stack.cards[cellView.cellIndex];
        
        let editor: CardEditorViewController = CardEditorViewController();
        editor.actionTitle = card.name;
        editor.actionDate = card.date;
        editor.actionComment = card.comment;
        editor.fields = card.fields;
        
        editor.completion = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            card.name = editor.actionTitle;
            card.date = editor.actionDate;
            card.comment = editor.actionComment;
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
        
        let stack: Stack = self.notebook!.stacks[stackView.stackIndex];
        let card: Card = stack.cards[cellView.cellIndex];
        
        stack.remove(card: card);
        self.reloadData();
    }

}

extension OutlineCentralViewController: ContentView {
    func reloadData() {
        self.saveData();
        self.outlineView.reloadData();
        
        self.broadcastNotebooks();
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
        WindowController.shared.dataSource = self;
    }
    
    private func loadNotebookData() {
        self.notebookManager.getInfo { (info: [NotebookInfo]?, error: NotebookManagerError?) in
            guard let info: [NotebookInfo] = info else {
                Log.error(message: "Could not retrieve notebooks info! - \(error!.localizedDescription)");
                self.presentError(error!);
                
                return;
            }
            
            guard !info.isEmpty else {
                self.notebook = Notebook(name: NSLocalizedString("Default", comment: ""));
                return;
            }
            
            self.notebookNames = info;
            WindowController.shared.refreshDisplay();
            
            self.loadNotebook(info: info.first!);
        }
    }
    
    private func loadNotebook(info: NotebookInfo) {
        self.notebookManager.retrieveNotebook(info: info) { (notebook: Notebook?, error: NotebookManagerError?) in
            guard let notebook: Notebook = notebook else {
                self.presentError(error!);
                return;
            }
            
            self.notebook = notebook;
            self.reloadData();
        }
    }
    
    private func displayLoadingView() {
        let alert: NSAlert = NSAlert();
        alert.messageText = "Test";
        alert.runModal();
    }
    
    private func saveData() {
        guard let notebook: Notebook = self.notebook else {
            return;
        }
        
//        do {
//            try self.notebookManager.saveNotebook(notebook: notebook);
//        } catch let e {
//            Log.error(message: "Could not save notebook with name \(notebook.name)");
//            self.presentError(e);
//        }
        
        self.notebookManager.saveNotebook(notebook: notebook) { (error: NotebookManagerError?) in
            if let error: NotebookManagerError = error {
                self.presentError(error);
                return;
            }
        }
    }
    
    private func broadcastNotebooks() {
        notebookManager.retrieveAllNotebooks { (notebooks: [Notebook]?, error: NotebookManagerError?) in
            guard error == nil else {
                self.presentError(error!);
                return;
            }
            
            guard let notebooks: [Notebook] = notebooks else {
                return;
            }
            
            self.broadcaster.broadcastNotebooks(notebooks: notebooks);
        }
    }
}

extension OutlineCentralViewController: WindowControllerDataSource, WindowControllerDelegate {
    func notebooks() -> [String] {
        return self.notebookNames.map({ (info: NotebookInfo) -> String in
            return info.name;
        });
    }
    
    func onAdd() {
        guard let notebook: Notebook = self.notebook else {
            return;
        }
        
        notebook.insertNewStack();
        self.reloadData();
    }
    
    func onRemove() {
        guard let notebook: Notebook = self.notebook else {
            return;
        }
        
        let controller: StackRemoveViewController = StackRemoveViewController();
        controller.stacks = notebook.stacks.map({ (iter: Stack) -> String in
            return iter.name;
        })
        controller.callback = { (ok: Bool) in
            guard ok else {
                return;
            }
            
            for name: String in controller.removedStacks {
                notebook.remove(named: name);
            }
            
            self.reloadData();
        }
        
        self.presentAsSheet(controller);
    }
    
    func onEdit() {
        
    }
    
    func onNotebookMenu() {
        let vc: NotebookManagerViewController = NotebookManagerViewController();
        self.presentAsSheet(vc);
    }
    
    func onRefresh() {
        self.loadNotebookData();
        self.broadcastNotebooks();
    }
    
    func onNotebookChanged(oldName: String?, newName: String) {
        let info: NotebookInfo? = notebookNames.first { (iter: NotebookInfo) -> Bool in
            return iter.name == newName;
        }
        
        guard let notebookInfo: NotebookInfo = info else {
            Log.error(message: "Could not find notebook with name \(newName)");
            return;
        }
        
        self.loadNotebook(info: notebookInfo);
        self.broadcastNotebooks();
    }
    
    func onSearch(term: String) {
        
    }
    
    func enabledItems() -> [Bool] {
        return [];
    }
}

extension OutlineCentralViewController : SourceChangedDelegate {
    func changed(type: NotebookManagerType) {
        notebookManager = NotebookManagerFactory.create(type: type);
        notebookManager.setup { (error: NotebookManagerError?) in
            if let error: NotebookManagerError = error {
                self.presentError(error);
                return;
            }
            
            self.loadNotebookData();
            self.broadcastNotebooks();
        }
    }
}
