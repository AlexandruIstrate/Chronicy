//
//  SafariExtensionViewController.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 12/03/2019.
//

import SafariServices;

class SafariExtensionViewController: SFSafariExtensionViewController {
    
    @IBOutlet private weak var notebookDropdown: NSPopUpButton!;
    @IBOutlet private weak var taskDropdown: NSPopUpButton!
    @IBOutlet private weak var enabledCheckbox: NSButton!;
    
    public static let shared: SafariExtensionViewController = {
        let shared: SafariExtensionViewController = SafariExtensionViewController();
        shared.preferredContentSize = NSSize(width: 360, height: 120);
        return shared;
    }();

    override func viewDidLoad() {
        super.viewDidLoad();
        
        loadTasks();
        displayTasks();
        displayState();
    }
    
    @IBAction func onTaskChanged(_ sender: NSPopUpButton) {
        guard let taskName: String = sender.selectedItem?.title else {
            return;
        }
        
        ContentTrackerManager.manager.sendData(data: taskName, trackerType: .task);
    }
    
    @IBAction func onTrackingChanged(_ sender: NSButton) {
        ExtensionStateManager.manager.state = ((sender.state == .on) ? .enabled : .disabled);
    }
}

extension SafariExtensionViewController {
    private func loadTasks() {
        TaskManager.manager.load();
    }
    
    private func displayTasks() {
        self.taskDropdown.removeAllItems();
        
        for task: Task in TaskManager.manager.tasks {
            self.taskDropdown.addItem(withTitle: task.name);
        }
    }
    
    private func displayState() {
        let state: ExtensionStateManager.State = ExtensionStateManager.manager.state;
        self.enabledCheckbox.state = (state == .enabled ? .on : .off);
    }
}
