//
//  CustomField.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/04/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

extension CustomField {
    public var customView: NSView? {
        let view: NSView?;
        
        switch self {
        case is TextField:
            view = (self as! TextField).customView;
        case is NumericField:
            view = (self as! NumericField).customView;
        default:
            view = nil;
        }
        
        return view;
    }
    
    public var icon: NSImage? {
        let icon: NSImage?;
        
        switch self {
        case is TextField:
            icon = (self as! TextField).icon;
        case is NumericField:
            icon = (self as! NumericField).icon;
        default:
            icon = nil;
        }
        
        return icon;
    }
}

extension TextField {
    public var customView: NSView? {
        let viewController: TextEditorViewController = TextEditorViewController();
        
        let view: NSView = viewController.view;
        view.translatesAutoresizingMaskIntoConstraints = false;
        return view;
    }
    
    public var icon: NSImage? {
        return NSImage(named: NSImage.Name(""));
    }
}

extension NumericField {
    public var customView: NSView? {
        let viewController: NumericEditorViewController = NumericEditorViewController();
        
        let view: NSView = viewController.view;
        view.translatesAutoresizingMaskIntoConstraints = false;
        return view;
    }
    
    public var icon: NSImage? {
        return NSImage(named: NSImage.Name(""));
    }
}
