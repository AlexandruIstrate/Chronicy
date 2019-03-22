//
//  SidebarTableCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 21/03/2019.
//

import Cocoa;

class SidebarTableCellView: NSTableCellView {
    
}

class SidebarTableCellData: NSObject {
    @objc public dynamic var title: String;
    @objc public dynamic var icon: NSImage;
    
    init(title: String, icon: NSImage) {
        self.title = title;
        self.icon = icon;
    }
}
