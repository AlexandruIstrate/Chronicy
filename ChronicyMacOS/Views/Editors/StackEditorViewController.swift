//
//  StackEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa;

class StackEditorViewController: NSViewController {

    @IBOutlet private weak var titleTextField: NSTextField!;
    @IBOutlet private weak var commentTextView: NSTextView!;
    
    public var taskTitle: String = String();
    public var taskComment: String = String();
    
    typealias TaskEditorCompletionHandler = (Bool) -> ();
    public var completion: TaskEditorCompletionHandler?;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.titleTextField.stringValue = self.taskTitle;
        self.commentTextView.string = self.taskComment;
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        self.taskTitle = self.titleTextField.stringValue;
        self.taskComment = self.commentTextView.string;
        
        completion?(true);
        self.dismiss(nil);
    }
    
    @IBAction private func onCancelPressed(_ sender: NSButton) {
        completion?(false);
        self.dismiss(nil);
    }
}
