//
//  SafariExtensionViewController.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 12/03/2019.
//

import SafariServices;

class SafariExtensionViewController: SFSafariExtensionViewController {
    
    @IBOutlet private var stateController: NSArrayController!;
    @IBOutlet private weak var stateDropDown: NSPopUpButton!;
    
    private static let availableStates: [ExtensionStateManager.State] = [.enabled, .disabled];
    
    public static let shared: SafariExtensionViewController = {
        let shared: SafariExtensionViewController = SafariExtensionViewController();
        shared.preferredContentSize = NSSize(width: 360, height: 120);
        return shared;
    }();

    override func viewDidLoad() {
        super.viewDidLoad();
        self.stateController.content = SafariExtensionViewController.availableStates.map({ (state: ExtensionStateManager.State) -> StateAdapter in
            return StateAdapter(state: state);
        });
    }
    
}
