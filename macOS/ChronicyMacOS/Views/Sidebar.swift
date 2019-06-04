//
//  Sidebar.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/04/2019.
//

import Cocoa;
import FatSidebar;

//struct SidebarItem {
//    var title: String;
//    var icon: NSImage;
//    var tint: NSColor?;
//
//    typealias SidebarItemClickHandler = (SidebarItem) -> ();
//    var clickHandler: SidebarItemClickHandler?;
//
//    init(title: String, icon: NSImage, tint: NSColor? = nil) {
//        self.title = title;
//        self.icon = icon;
//        self.tint = tint;
//    }
//
//    init(title: String, icon: NSImage, tint: NSColor? = nil, clickHandler: @escaping SidebarItemClickHandler) {
//        self.init(title: title, icon: icon, tint: tint);
//        self.clickHandler = clickHandler;
//    }
//}

struct SidebarTheme: FatSidebarTheme {
    
    let itemStyle: FatSidebarItemStyle = OmniFocusItemStyle();
    let sidebarBackground = SidebarTheme.backgroundColor;
    
    public static var selectedColor: NSColor { return NSColor(named: NSColor.Name("SidebarSelected")) ?? NSColor.white; }
    public static var recessedColor: NSColor { return NSColor(named: NSColor.Name("SidebarRecessed")) ?? NSColor.white; }
    public static var backgroundColor: NSColor { return NSColor(named: NSColor.Name("SidebarBackground")) ?? NSColor.white; }
    
    struct OmniFocusItemStyle: FatSidebarItemStyle {
        
        let font: NSFont? = NSFont.systemFont(ofSize: 14)
        let labelColor = StatefulColor(single: NSColor(named: NSColor.Name("SidebarText")) ?? NSColor.white);
        
        let background = StatefulBackgroundColor(
            normal:      SidebarTheme.backgroundColor,
            selected:    SidebarTheme.selectedColor,
            highlighted: SidebarTheme.recessedColor,
            backgroundStyle: .light)
        
        let borders = Borders(
            bottom: StatefulColor(single: SidebarTheme.recessedColor),
            right: StatefulColor(
                normal:      SidebarTheme.recessedColor,
                selected:    SidebarTheme.selectedColor,
                highlighted: SidebarTheme.recessedColor))
    }
}

extension Page: FatSidebarItemConvertible {
    public var configuration: FatSidebarItemConfiguration {
        let icon: NSImage!;
        
        if let tint: NSColor = self.tint {
            icon = self.icon.image(tintColor: tint);
        } else {
            icon = self.icon;
        }
        
        let item: FatSidebarItemConfiguration = FatSidebarItemConfiguration(title: self.title, image: icon, shadow: nil, animated: true) { (item: FatSidebarItem) in
            self.onLoadPage?(self);
        };
        
        return item;
    }
}
