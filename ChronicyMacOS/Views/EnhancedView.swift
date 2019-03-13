//
//  CustomView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 13/03/2019.
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
        get { return 0.0; }
        set {  }
    }
    
}

protocol CustomOperationSeparatable {
    func onLoadData();
    func onLayoutView();
}
