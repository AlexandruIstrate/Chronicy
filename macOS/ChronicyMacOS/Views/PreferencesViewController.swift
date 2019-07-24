//
//  PreferencesViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;
import Cocoa;

public class PreferencesViewController : NSTabViewController {
    
    public override func tabView(_ tabView: NSTabView, didSelect tabViewItem: NSTabViewItem?) {
        guard let tabViewItem: NSTabViewItem = tabViewItem else {
            return;
        }
        
        PreferencesWindowController.shared?.window?.title = tabViewItem.label;
    }
}
