//
//  SidebarView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class SidebarView: NSTableView {
    
    public static let defaultBackgroundColor: NSColor = NSColor(calibratedRed: 25, green: 25, blue: 25, alpha: 255);
    
//    public var rows: [SidebarTableCellData] = [ SidebarTableCellData(title: "AAA", icon: NSImage(byReferencing: URL(string: "127.0.01.")!)) ];

    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setup();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setup();
    }
    
}

extension SidebarView: NSTableViewDataSource, NSTableViewDelegate {
    
    private var cellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "SIdebarCell"); }
    
    public func numberOfRows(in tableView: NSTableView) -> Int {
//        return rows.count;
        return 5;
    }
    
    public func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let view: NSView = self.makeView(withIdentifier: self.cellIdentifier, owner: nil) else {
            Log.error(message: "Could not create view for SidebarView cell!");
            return nil;
        }
        
        guard let cell: SidebarTableCellView = view as? SidebarTableCellView else {
            Log.error(message: "Cannot convert SidebarView table view to SidebarTableCellView");
            return nil;
        }
        
        cell.title = "Test";
        
        return cell;
    }
    
}

extension SidebarView {
    private func setup() {
        setupView();
//        setupBinding();
        
        self.dataSource = self;
        self.delegate = self;
    }
    
    private func setupView() {
        self.backgroundColor = SidebarView.defaultBackgroundColor;
    }
    
    private func setupBinding() {
        let ac: NSArrayController = NSArrayController(content: rows);
        self.bind(NSBindingName.content, to: ac, withKeyPath: #keyPath(NSArrayController.arrangedObjects), options: nil);
    }
}
