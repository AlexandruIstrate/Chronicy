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
    
    override func viewDidLoad() {
        super.viewDidLoad();
    }
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
        self.text = textView.string;
    }
    
}
