//
//  PageManager.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/04/2019.
//

import Cocoa;

struct Page {
    var title: String;
    var icon: NSImage;
    var tint: NSColor?;
    
    typealias PageCallback = (Page) -> ();
    var onLoadPage: PageCallback?;
    var onUnloadPage: PageCallback?;
    
    init(title: String, icon: NSImage, tint: NSColor? = nil, onLoad: PageCallback? = nil, onUnload: PageCallback? = nil) {
        self.title = title;
        self.icon = icon;
        self.tint = tint;
        
        self.onLoadPage = onLoad;
        self.onUnloadPage = onUnload;
    }
}

extension Page: Equatable {
    static func == (lhs: Page, rhs: Page) -> Bool {
        return lhs.title == rhs.title;
    }
}

class PageManager {
    
    public private(set) var pages: [Page] = [];
    public private(set) var selectionIndex: Int?;
    
    public var selected: Page? {
        guard let index: Int = selectionIndex else {
            return nil;
        }
        
        guard self.pages.count > 0 else {
            return nil;
        }
        
        return self.pages[index];
    }
    
    public func add(page: Page) {
        self.pages.append(page);
    }
    
    public func remove(page: Page) {
        self.pages.removeAll { (iter: Page) -> Bool in
            return iter == page;
        }
    }
    
    @discardableResult
    public func select(page: Page) -> Bool {
        let index: Int? = self.pages.firstIndex(of: page);
        
        guard index != nil else {
            return false;
        }
        
        return self.select(at: index!);
    }
    
    @discardableResult
    public func select(at index: Int) -> Bool {
        guard (0..<self.pages.count).contains(index) else {
            return false;
        }
        
        self.selectionIndex = index;
        return true;
    }
}
