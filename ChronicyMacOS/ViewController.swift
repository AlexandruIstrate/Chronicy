//
//  ViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;

class ViewController: NSViewController, NSTextViewDelegate {

    /// - Tag: setRepresentedObjectExample
    override var representedObject: Any? {
        didSet {
            // Pass down the represented object to all of the child view controllers.
            for child in children {
                child.representedObject = representedObject
            }
        }
    }

    weak var document: Document? {
        if let docRepresentedObject = representedObject as? Document {
            return docRepresentedObject
        }
        return nil
    }

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }

    override func viewDidAppear() {
        super.viewDidAppear()
    }

    // MARK: - NSTextViewDelegate

    func textDidBeginEditing(_ notification: Notification) {
        document?.objectDidBeginEditing(self)
    }

    func textDidEndEditing(_ notification: Notification) {
        document?.objectDidEndEditing(self)
    }

}
