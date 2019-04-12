//
//  CustomField.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/04/2019.
//

import Cocoa;
import ChronicyFramework;

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
}

extension TextField {
    public var customView: NSView? {
        return NSButton(title: "Text", target: nil, action: nil);
    }
}

extension NumericField {
    public var customView: NSView? {
        return NSButton(title: "Numeric", target: nil, action: nil);
    }
}
