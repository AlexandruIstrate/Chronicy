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
        } else if let content: String = sidebarItem.content {
            cell.textField?.stringValue = content;
        } else {
            Log.warining(message: "SidebarItem should have either a header or some content");
        }
        
        cell.imageView?.image = sidebarItem.icon;
        
        return cell;
    }
    
    func outlineViewSelectionDidChange(_ notification: Notification) {
        let selectedItem: SidebarItem = self.items[self.selectedRow];
        selectedItem.callback?(selectedItem, self);
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
        self.add(sidebarItem: SidebarItem(header: "Navigate"));
        
        self.add(sidebarItem: SidebarItem(content: "Outline", icon: NSImage(named: NSImage.Name("Outline"))!, callback: { (sidebarItem: SidebarItem, outlineView: NSOutlineView) in
            MainViewController.shared.showCenterView(viewController: OutlineCentralViewController());
        }));
        self.add(sidebarItem: SidebarItem(content: "Timeline", icon: NSImage(named: NSImage.Name("Timeline"))!, callback: { (sidebarItem: SidebarItem, outlineView: NSOutlineView) in
            
        }));
        self.add(sidebarItem: SidebarItem(content: "Trackers", icon: NSImage(named: NSImage.Name("Trackers"))!, callback: { (sidebarItem: SidebarItem, outlineView: NSOutlineView) in
            MainViewController.shared.showCenterView(viewController: ModuleManagerViewController());
        }));
    }
}

struct SidebarItem {
    var header: String?;
    var content: String?;
    var icon: NSImage?;
    
    typealias SidebarItemCallback = (SidebarItem, NSOutlineView) -> ();
    var callback: SidebarItemCallback?;
    
    init(header: String? = nil, content: String? = nil, icon: NSImage? = nil, callback: SidebarItemCallback? = nil) {
        self.header = header;
        self.content = content;
        self.icon = icon;
        self.callback = callback;
    }
}
