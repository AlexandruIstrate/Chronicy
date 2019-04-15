//
//  TextEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 15/04/2019.
//

import Cocoa

class TextEditorViewController: NSViewController {

    @IBOutlet private var textView: NSTextView!;
    
    public var text: String = "";
    
}

// Delegate set in IB
extension TextEditorViewController: NSTextViewDelegate {
    func textDidChange(_ notification: Notification) {
        self.text = textView.string;
    }
}
