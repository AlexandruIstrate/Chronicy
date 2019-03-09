//
//  ViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;

class ViewController: NSViewController, NSTextViewDelegate {

    override var representedObject: Any? {
        didSet {
            // Pass down the represented object to all of the child view controllers.
            for child: NSViewController in children {
                child.representedObject = representedObject;
            }
        }
    }

    weak var document: Document? {
        if let docRepresentedObject: Document = representedObject as? Document {
            return docRepresentedObject;
        }
        
        return nil;
    }

    // MARK: - NSTextViewDelegate

    func textDidBeginEditing(_ notification: Notification) {
        document?.objectDidBeginEditing(self);
    }

    func textDidEndEditing(_ notification: Notification) {
        document?.objectDidEndEditing(self);
    }

}
