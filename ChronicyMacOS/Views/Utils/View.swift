//
//  View.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;

extension NSView {
    
    @IBInspectable
    public var background: NSColor {
        get { return NSColor(cgColor: layer?.backgroundColor ?? CGColor.white)!; }
        set { self.wantsLayer = true; self.layer?.backgroundColor = newValue.cgColor; }
    }
    
    @IBInspectable
    public var cornerRadius: CGFloat {
        get { return self.layer?.cornerRadius ?? 0.0; }
        set { self.wantsLayer = true; self.layer?.cornerRadius = newValue; }
    }
    
}

protocol CustomOperationSeparatable {
    func onLoadData();
    func onLayoutView();
}
