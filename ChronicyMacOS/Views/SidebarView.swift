//
//  SidebarView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class SidebarView: NSOutlineView {
    
    public static let defaultBackgroundColor: NSColor = NSColor(calibratedRed: 25, green: 25, blue: 25, alpha: 255);
    
    private var items: [SidebarItem] = [];

    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setup();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setup();
    }
    
    public func add(sidebarItem: SidebarItem) {
        self.items.append(sidebarItem);
    }
    
}

extension SidebarView: NSOutlineViewDataSource, NSOutlineViewDelegate {
    
    private var headerCellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "SidebarViewHeaderCell"); }
    private var contentCellIdentifier: NSUserInterfaceItemIdentifier { return NSUserInterfaceItemIdentifier(rawValue: "SidebarViewCell"); }
    
    public func outlineView(_ outlineView: NSOutlineView, numberOfChildrenOfItem item: Any?) -> Int {
        return items.count;
    }
    
    public func outlineView(_ outlineView: NSOutlineView, child index: Int, ofItem item: Any?) -> Any {
        return items[index];
    }
    
    public func outlineView(_ outlineView: NSOutlineView, isItemExpandable item: Any) -> Bool {
        return false;
    }
    
    public func outlineView(_ outlineView: NSOutlineView, viewFor tableColumn: NSTableColumn?, item: Any) -> NSView? {
        guard let sidebarItem: SidebarItem = item as? SidebarItem else {
            Log.error(message: "Sidebar item is not of the compatible type!");
            return nil;
        }
        
        guard let view: NSView = outlineView.makeView(withIdentifier: ((sidebarItem.header != nil) ? self.headerCellIdentifier : self.contentCellIdentifier), owner: self) else {
            Log.error(message: "Could not create view for SidebarView cell!");
            return nil;
        }
        
        guard let cell: NSTableCellView = view as? NSTableCellView else {
            Log.error(message: "Cannot convert SidebarView table view to NSTableCellView");
            return nil;
        }
        
        if let header: String = sidebarItem.header {
            cell.textField?.stringValue = header.uppercased();
        } else if let content: String = sidebarItem.header {
            cell.textField?.stringValue = content;
        } else {
            Log.warining(message: "SidebarItem should have either a header or some content");
            return nil;
        }
        
        return cell;
    }
}

extension SidebarView {
    private func setup() {
        setupView();
        setupSidebarItems();
        setupSources();
    }
    
    private func setupView() {
        self.backgroundColor = SidebarView.defaultBackgroundColor;
    }
    
    private func setupSources() {
        self.dataSource = self;
        self.delegate = self;
    }
    
    private func setupSidebarItems() {
        self.add(sidebarItem: SidebarItem(header: "Items"));
        self.add(sidebarItem: SidebarItem(content: "Test"));
        self.add(sidebarItem: SidebarItem(content: "Test"));
        self.add(sidebarItem: SidebarItem(content: "Test"));
    }
}

struct SidebarItem {
    var header: String?;
    var content: String?;
    var data: Any?;
    
    typealias SidebarItemCallback = (SidebarItem, NSOutlineView) -> ();
    var callback: SidebarItemCallback?;
    
    init(header: String?, content: String?, data: Any?, callback: SidebarItemCallback?) {
        self.header = header;
        self.content = content;
        self.data = data;
        self.callback = callback;
    }
    
    init(header: String, data: Any? = nil, callback: SidebarItemCallback? = nil) {
        self.init(header: header, content: nil, data: data, callback: callback);
    }
    
    init(content: String, data: Any? = nil, callback: SidebarItemCallback? = nil) {
        self.init(header: nil, content: content, data: data, callback: callback);
    }
}
