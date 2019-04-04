//
//  View.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;

protocol AdvancedView {
    var backgroundColor: NSColor { get set }
    
    var shadowOpacity: Float { get set }
    var shadowColor: NSColor { get set }
    var shadowOffset: CGSize { get set }
    var shadowRadius: CGFloat { get set }
    
    var cornerRadius: CGFloat { get set }
}

extension NSView: AdvancedView {
    @IBInspectable
    public var backgroundColor: NSColor {
        get { return NSColor(cgColor: self.layer?.backgroundColor ?? CGColor.white)!; }
        set { self.wantsLayer = true; self.layer?.backgroundColor = newValue.cgColor; }
    }
    
    @IBInspectable
    public var shadowOpacity: Float {
        get { return self.layer?.shadowOpacity ?? 0.0; }
        set { self.wantsLayer = true; self.layer?.shadowOpacity = newValue; }
    }
    
    @IBInspectable
    public var shadowColor: NSColor {
        get { return NSColor(cgColor: self.layer?.shadowColor ?? CGColor.white)!; }
        set { self.wantsLayer = true; self.layer?.shadowColor = newValue.cgColor; }
    }
    
    @IBInspectable
    public var shadowOffset: CGSize {
        get { return self.layer?.shadowOffset ?? CGSize(); }
        set { self.wantsLayer = true; self.layer?.shadowOffset = newValue; }
    }
    
    @IBInspectable
    public var shadowRadius: CGFloat {
        get { return self.layer?.shadowRadius ?? 0.0; }
        set { self.wantsLayer = true; self.layer?.shadowRadius = newValue; }
    }
    
    @IBInspectable
    public var cornerRadius: CGFloat {
        get { return self.layer?.cornerRadius ?? 0.0; }
        set { self.wantsLayer = true; self.layer!.cornerRadius = newValue; }
    }
}

protocol CustomOperationSeparatable {
    func onLoadData();
    func onLayoutView();
}

protocol ViewInteractionDelegate {
    func onClick(at point: CGPoint, in view: NSView);
    func onRightClick(at point: CGPoint, in view: NSView);
}
