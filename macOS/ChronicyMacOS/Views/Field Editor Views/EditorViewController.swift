//
//  EditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 01/05/2019.
//

import Cocoa;

class EditorViewController: NSViewController {
    typealias EditorCallback = (EditorViewController) -> ();
    public var callback: EditorCallback?;
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
        callback?(self);
    }
}
