//
//  SafariExtensionViewController.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 12/03/2019.
//

import SafariServices;

class SafariExtensionViewController: SFSafariExtensionViewController {
    
    @IBOutlet private weak var taskDropdown: NSPopUpButton!;
    @IBOutlet private weak var enabledCheckbox: NSButton!;
    
    public static let shared: SafariExtensionViewController = {
        let shared: SafariExtensionViewController = SafariExtensionViewController();
        shared.preferredContentSize = NSSize(width: 360, height: 120);
        return shared;
    }();

    override func viewDidLoad() {
        super.viewDidLoad();
        setupData();
    }
    
    @IBAction func onTaskChanged(_ sender: NSPopUpButton) {
        
    }
    
    @IBAction func onTrackingChanged(_ sender: NSButton) {
        ExtensionStateManager.manager.state = ((sender.state == NSControl.StateValue.on) ? .enabled : .disabled);
    }
}

extension SafariExtensionViewController {
    private func setupData() {
        TaskManager.manager.load();
        
        for task: Task in TaskManager.manager.tasks {
            self.taskDropdown.addItem(withTitle: task.name);
        }
    }
}
