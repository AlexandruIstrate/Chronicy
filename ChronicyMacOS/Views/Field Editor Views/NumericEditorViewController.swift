//
//  NumericEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 15/04/2019.
//

import Cocoa

class NumericEditorViewController: NSViewController {
    
    @IBOutlet private weak var numberField: NSTextField!;
    @IBOutlet private weak var numberFormatter: NumberFormatter!;
    
    public var value: Float = 0.0;
    
    @IBAction private func onDecimalCheckboxToggled(_ sender: NSButton) {
        switch sender.state {
        case NSControl.StateValue.on:
            numberFormatter.numberStyle = .decimal;
            
        case NSControl.StateValue.off:
            numberFormatter.numberStyle = .none;
            
        default:
            break;
        }
    }
    
}
