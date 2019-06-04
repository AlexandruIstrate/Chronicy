//
//  WindowController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class WindowController: NSWindowController, NSWindowDelegate {
    
    public private(set) static var shared: WindowController!;
    
    @IBOutlet private weak var addButton: NSButton!;
    @IBOutlet private weak var removeButton: NSButton!;
    @IBOutlet private weak var editButton: NSButton!;
    
    @IBOutlet private weak var notebookPopUp: NSPopUpButton!;
    
    private var currentNotebookName: String?;
    
    public var delegate: WindowControllerDelegate?;
    public var dataSource: WindowControllerDataSource? { didSet { self.reloadNotebooks(); } }
    
    public var canAdd: Bool = true { didSet { self.addButton.isEnabled = self.canAdd; } }
    public var canRemove: Bool = true { didSet { self.removeButton.isEnabled = self.canRemove; } }
    public var canEdit: Bool = true { didSet { self.editButton.isEnabled = self.canEdit; } }
    
    public var mainViewController: MainViewController {
        guard let vc: NSViewController = self.contentViewController else {
            Log.fatal(message: "Could not retrieve main controller!");
            fatalError();
        }
        
        guard let mainVC: MainViewController = vc as? MainViewController else {
            Log.fatal(message: "Main controller is not of the appropiate type!");
            fatalError();
        }
        
        return mainVC;
    }
    
    override init(window: NSWindow?) {
        super.init(window: window);
        setupSharedInstance();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setupSharedInstance();
    }
    
    @IBAction private func onAdd(_ sender: NSButton) {
        delegate?.onAdd();
    }
    
    @IBAction private func onRemove(_ sender: NSButton) {
        delegate?.onRemove();
    }
    
    @IBAction private func onEdit(_ sender: NSButton) {
        delegate?.onEdit();
    }
    
    @IBAction private func onNotebookChanged(_ sender: NSPopUpButton) {
        let newValue: String = sender.stringValue;
        delegate?.onNotebookChanged(oldName: self.currentNotebookName, newName: newValue);
        self.currentNotebookName = newValue;
    }
    
    @IBAction private func onNotebooksMenuClicked(_ sender: NSButton) {
        delegate?.onNotebookMenu();
    }
    
    @IBAction private func onRefresh(_ sender: NSButton) {
        delegate?.onRefresh();
        self.reloadNotebooks();
    }
    
    @IBAction private func onSearch(_ sender: NSSearchField) {
        delegate?.onSearch(term: sender.stringValue);
    }
    
    private func setupPopUp() {
        self.notebookPopUp.removeAllItems();
    }
    
    private func setupSharedInstance() {
        WindowController.shared = self;
    }
    
    private func reloadNotebooks() {
        guard let notebooks: [String] = dataSource?.notebooks() else {
            Log.info(message: "No data source set for window");
            return;
        }

        self.notebookPopUp.removeAllItems();
        self.notebookPopUp.addItems(withTitles: notebooks);
    }
    
    public func refreshDisplay() {
        self.reloadNotebooks();
    }
}

protocol WindowControllerDelegate {
    func onAdd();
    func onRemove();
    func onEdit();
    func onNotebookMenu();
    func onRefresh();
    
    func onNotebookChanged(oldName: String?, newName: String);
    func onSearch(term: String);
    
    // TODO: Change return type
    func enabledItems() -> [Bool];
}

protocol WindowControllerDataSource {
    func notebooks() -> [String];
}
