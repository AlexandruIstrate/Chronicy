//
//  CardEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Cocoa

class CardEditorViewController: NSViewController {

    @IBOutlet private weak var titleTextField: NSTextField!;
    @IBOutlet private var commentTextView: NSTextView!;
    @IBOutlet private weak var datePicker: NSDatePicker!;
    
    @IBOutlet private weak var colorWell: NSColorWell!;
    @IBOutlet private weak var iconImageView: NSImageView!;
    
    public var actionTitle: String = String();
    public var actionComment: String = String();
    public var actionDate: Date = Date();
    
    public var actionColor: NSColor = NSColor.white;
    public var actionIcon: NSImage?;
    
    typealias ActionEditorCompletionHandler = (Bool) -> ();
    public var completion: ActionEditorCompletionHandler?;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.titleTextField.stringValue = self.actionTitle;
        self.commentTextView.string = self.actionComment;
        self.datePicker.dateValue = self.actionDate;
        
        self.colorWell.color = self.actionColor;
        self.iconImageView.image = self.actionIcon;
    }
    
    @IBAction private func onOKPressed(_ sender: NSButton) {
        self.actionTitle = self.titleTextField.stringValue;
        self.actionComment = self.commentTextView.string;
        self.actionDate = self.datePicker.dateValue;
        
        self.actionColor = self.colorWell.color;
        self.actionIcon = self.iconImageView.image;

        self.dismiss(nil);
        completion?(true);
    }
    
    @IBAction func onCancelPressed(_ sender: NSButton) {
        self.dismiss(nil);
        completion?(false);
    }
    
}
