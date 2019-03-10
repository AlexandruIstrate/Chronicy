//
//  ViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFramework;

class ViewController: NSViewController, NSTextViewDelegate {
    
    @IBOutlet private weak var timelineView: TimelineView!;
    
    private var timeline: Timeline = Timeline(name: "Test");

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
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.timelineView.dataSource = self;
    }

    // MARK: - NSTextViewDelegate

    func textDidBeginEditing(_ notification: Notification) {
        document?.objectDidBeginEditing(self);
    }

    func textDidEndEditing(_ notification: Notification) {
        document?.objectDidEndEditing(self);
    }

}

extension ViewController: TimelineViewDataSource {
    func stackCount() -> Int {
        return 0;
    }
    
    func stack(at index: Int) -> TimelineStackView {
        return TimelineStackView();
    }
}
