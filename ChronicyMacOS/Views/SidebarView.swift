//
//  SidebarView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;

class SidebarView: NSTableView {
    
    public static let defaultBackgroundColor: NSColor = NSColor(calibratedRed: 25, green: 25, blue: 25, alpha: 255);
    
    public var rows: [SidebarTableCellData] = [];

    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setupView();
        setupBinding();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setupView();
        setupBinding();
    }
    
}

extension SidebarView: NSTableViewDataSource {
    
}

extension SidebarView {
    private func setupView() {
        self.backgroundColor = SidebarView.defaultBackgroundColor;
    }
    
    private func setupBinding() {
        let ac: NSArrayController = NSArrayController(content: rows);
        self.bind(NSBindingName.content, to: ac, withKeyPath: #keyPath(NSArrayController.arrangedObjects), options: nil);
    }
}
