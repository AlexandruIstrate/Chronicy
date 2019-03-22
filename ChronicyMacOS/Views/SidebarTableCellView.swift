//
//  SidebarTableCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 21/03/2019.
//

import Cocoa;

class SidebarTableCellView: NSTableCellView {
    
    public var title: String {
        get { return self.textField?.stringValue ?? ""; }
        set { self.textField?.stringValue = newValue; }
    }
    
    public var icon: NSImage {
        get { return self.imageView!.image!; }
        set { self.imageView?.image = newValue; }
    }
}
