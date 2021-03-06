//
//  SafariExtensionViewController.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 12/03/2019.
//

import SafariServices;

class SafariExtensionViewController: SFSafariExtensionViewController {
    
    @IBOutlet private weak var notebookDropdown: NSPopUpButton!;
    @IBOutlet private weak var stackDropdown: NSPopUpButton!
    @IBOutlet private weak var enabledCheckbox: NSButton!;
    
    public static let shared: SafariExtensionViewController = {
        let shared: SafariExtensionViewController = SafariExtensionViewController();
        shared.preferredContentSize = NSSize(width: 360, height: 120);
        return shared;
    }();
    
    public var selectedNotebook: ExtensionNotebook? {
        guard let notebookName: String = notebookDropdown.selectedItem?.title else {
            Log.info(message: "No notebook selected!");
            return nil;
        }
        
        guard let notebook: ExtensionNotebook = NotebookCollection.collection.named(name: notebookName) else {
            Log.info(message: "No notebook with name \(notebookName)!");
            return nil;
        }
        
        return notebook;
    }
    
    public var selectedStack: ExtensionStack? {
        guard let stackName: String = stackDropdown.selectedItem?.title else {
            Log.info(message: "No stack selected!");
            return nil;
        }
        
        let ret: ExtensionStack? = self.selectedNotebook?.named(name: stackName);
        return ret;
    }
    
    private var lastNotebook: ExtensionNotebook?;
    private var lastStack: ExtensionStack?;
    
    override func viewDidAppear() {
        super.viewDidAppear();
        
        loadData();
        displayData();
        displayState();
        
        updateApplicationSelectedValues();
    }
    
    @IBAction private func onNotebookChanged(_ sender: NSPopUpButton) {
        lastNotebook = selectedNotebook;
        updateApplicationSelectedValues();
        self.displayStacks();
    }
    
    @IBAction private func onStackChanged(_ sender: NSPopUpButton) {
        lastStack = selectedStack;
        updateApplicationSelectedValues();
    }
    
    @IBAction private func onTrackingChanged(_ sender: NSButton) {
        updateApplicationSelectedValues();
        ExtensionStateManager.manager.state = ((sender.state == .on) ? .enabled : .disabled);
    }
}

extension SafariExtensionViewController {
    private func loadData() {
        NotebookCollection.collection.load();
    }
    
    private func displayData() {
        displayNotebooks();
        displayStacks();
    }
    
    private func displayNotebooks() {
        self.notebookDropdown.removeAllItems();
        
        self.notebookDropdown.addItems(withTitles: NotebookCollection.collection.notebooks.map({ (iter: ExtensionNotebook) -> String in
            return iter.name;
        }));
        
        ensureNotebookSelected();
    }
    
    private func displayStacks() {
        self.stackDropdown.removeAllItems();
        
        guard let notebook: ExtensionNotebook = self.selectedNotebook else {
            Log.error(message: "Could not get notebook with name!");
            return;
        }
        
        self.stackDropdown.addItems(withTitles: notebook.stacks.filter({ (iter: ExtensionStack) -> Bool in
            // Only use stacks that support our fields
            return iter.definition.matches(other: CustomDefinitions.url);
//            return true;
        }).map({ (stack: ExtensionStack) -> String in
            return stack.name;
        }));
        
        ensureStackSelected();
    }
    
    private func displayState() {
        let state: ExtensionStateManager.State = ExtensionStateManager.manager.state;
        self.enabledCheckbox.state = (state == .enabled ? .on : .off);
    }
    
    private func updateApplicationSelectedValues() {
        guard let notebook: ExtensionNotebook = self.selectedNotebook else {
            return;
        }
        
        guard let stack: ExtensionStack = self.selectedStack else {
            return;
        }
        
        ContentTrackerManager.manager.sendData(data: notebook.name, trackerType: .notebook);
        ContentTrackerManager.manager.sendData(data: stack.name, trackerType: .stack);
    }
    
    private func ensureNotebookSelected() {
        if let selected: ExtensionNotebook = lastNotebook {
            if notebookDropdown.itemTitles.contains(selected.name) {
                self.notebookDropdown.selectItem(withTitle: selected.name);
                return;
            }
        }
        
        if notebookDropdown.itemArray.count > 0 {
            let first: String = self.notebookDropdown.itemTitles.first!;
            self.notebookDropdown.selectItem(withTitle: first);
        }
    }
    
    private func ensureStackSelected() {
        if let selected: ExtensionStack = lastStack {
            if stackDropdown.itemTitles.contains(selected.name) {
                self.stackDropdown.selectItem(withTitle: selected.name);
                return;
            }
        }
        
        if stackDropdown.itemArray.count > 0 {
            let first: String = self.stackDropdown.itemTitles.first!;
            self.stackDropdown.selectItem(withTitle: first);
        }
    }
}
