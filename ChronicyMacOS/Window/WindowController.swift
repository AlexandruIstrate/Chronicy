//
//  WindowController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class WindowController: NSWindowController, NSWindowDelegate {
    
    private let notificationCenter: NotificationCenter = NotificationCenter.default;
    
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
 
    @IBAction func onAdd(_ sender: Any) {
        notificationCenter.post(name: Notifications.toolbarAdd.name, object: self, userInfo: nil);
    }

    @IBAction func onRemove(_ sender: Any) {
        notificationCenter.post(name: Notifications.toolbarRemove.name, object: self, userInfo: nil);
    }

    @IBAction func onEdit(_ sender: Any) {
        notificationCenter.post(name: Notifications.toolbarEdit.name, object: self, userInfo: nil);
    }

}

extension WindowController {
    public enum Notifications: String, NotificationName {
        case toolbarAdd;
        case toolbarRemove;
        case toolbarEdit;
    }
}
