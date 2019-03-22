//
//  TimelineCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;

@IBDesignable
class OutlineCellView: NSView {

    @IBOutlet private weak var titleLabel: NSTextField!;
    
    public var title: String {
        get { return titleLabel.stringValue; }
        set { self.titleLabel.stringValue = newValue; }
    }
    
    override func draw(_ dirtyRect: NSRect) {
        super.draw(dirtyRect);
        
//        self.layer?.shadowOpacity = shadowOpacity;
//        self.layer?.shadowColor = shadowColor.cgColor;
//        self.layer?.shadowOffset = CGSize.zero;
//        self.layer?.shadowRadius = shadowBlur;
//        self.layer?.cornerRadius = cardRadius;
        
//        backgroundIV.image = backgroundImage
//        backgroundIV.layer.cornerRadius = self.layer.cornerRadius
//        backgroundIV.clipsToBounds = true
//        backgroundIV.contentMode = .scaleAspectFill
//
//        backgroundIV.frame.origin = bounds.origin
//        backgroundIV.frame.size = CGSize(width: bounds.width, height: bounds.height)
//        contentInset = 6
    }
}

extension OutlineCellView: CustomOperationSeparatable {
    func onLoadData() {
        
    }
    
    func onLayoutView() {
        
    }
}
