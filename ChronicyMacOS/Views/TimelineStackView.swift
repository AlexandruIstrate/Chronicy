//
//  TimelineStackView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa

class TimelineStackView: NSView {
    
    private var cells: [TimelineCellView] = [];

    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setupView();
    }
    
    required init?(coder decoder: NSCoder) {
        super.init(coder: decoder);
        setupView();
    }
    
}

extension TimelineStackView {
    private func setupView() {
//        self.addSubview(TimelineCellView(frame: NSRect(x: 0, y: 0, width: 800, height: 600)));
        
//        NSNib.init(nibNamed: NSNib.Name("TimelineStackView"), bundle: nil)?.instantiate(withOwner: self, topLevelObjects: nil);
//        self.addSubview(NSButton(frame: NSRect(x: 0, y: 0, width: 100, height: 100)));
        
//        guard let nib: NSNib = NSNib.init(nibNamed: "TimelineStackView", bundle: nil) else {
//            fatalError("Could not find NIB!");
//        }
//        
//        var topLevelObjects: NSArray = NSArray(array: []);
//        
//        guard nib.instantiate(withOwner: self, topLevelObjects: topLevelObjects) else {
//            fatalError("Could not instantiate NIB!");
//        }
    }
}
