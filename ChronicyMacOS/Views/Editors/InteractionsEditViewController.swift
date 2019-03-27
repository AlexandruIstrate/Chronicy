//
//  InteractionsEditViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 27/03/2019.
//

import Cocoa;
import ChronicyFramework;

// TODO: Refactor this to something that isn't hardcoded and inflexible
class InteractionsEditViewController: NSViewController {
    
    @IBOutlet private weak var actionsPopUp: NSPopUpButton!;
    
    private var selectedAction: ModuleTrigger.TriggerAction?;
    public var action: ModuleTrigger.TriggerAction?;
    
    typealias InteractionsEditorCompletionHandler = (Bool) -> ();
    public var completion: InteractionsEditorCompletionHandler?;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        loadData();
    }
    
    @IBAction private func onActionChanged(_ sender: NSPopUpButton) {
        switch sender.indexOfSelectedItem {
        case 0:
            self.selectedAction = { (trigger: ModuleTrigger) in
                NotificationSender.default.show(title: "New Action", subtitle: "You have accesed a web page");
            }
            
        case 1:
            self.selectedAction = { (trigger: ModuleTrigger) in
                do {
                    try ApplicationManager.manager.launch(defaultApplication: .terminal);
                } catch let e {
                    Log.error(message: "Could not open Terminal!");
                    self.presentError(e);
                }
            }
            
        default:
            break;
        }
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        self.dismiss(nil);
        self.action = self.selectedAction;
        self.completion?(true);
    }
    
    @IBAction private func onCancelPressed(_ sender: NSButton) {
        self.dismiss(nil);
        self.completion?(false);
    }
    
}

extension InteractionsEditViewController {
    private func loadData() {
        // TODO: Use data from the model
        let items: [String] = ["Show a Notification", "Open Terminal"];
        self.actionsPopUp.addItems(withTitles: items);
    }
}
