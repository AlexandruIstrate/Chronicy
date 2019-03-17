//
//  WindowController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;

// TODO: Remove "Show Tabs" from application menu
class WindowController: NSWindowController, NSWindowDelegate {
    
    required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        /** NSWindows loaded from the storyboard will be cascaded
         based on the original frame of the window in the storyboard.
         */
        shouldCascadeWindows = true;
    }
    
}

extension WindowController {
    @IBAction private func onAdd(_ sender: Any) {
        
    }
    
    @IBAction private func onRemove(_ sender: Any) {
        
    }
    
    @IBAction private func onShowEditPane(_ sender: Any) {
        
    }
    
    @IBAction private func onChangeViewMode(_ sender: Any) {
        
    }
}
