//
//  TextEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 15/04/2019.
//

import Cocoa;

class TextEditorViewController: EditorViewController, NSTextViewDelegate {

    @IBOutlet private var textView: NSTextView!;
    public var text: String = "";
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupView();
    }
    
    func textDidChange(_ notification: Notification) {
        self.text = textView.string;
    }
    
    private func setupView() {
        self.textView.delegate = self;
        self.textView.string = self.text;
    }
}
