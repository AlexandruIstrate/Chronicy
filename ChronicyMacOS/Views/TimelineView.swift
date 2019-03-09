//
//  TimelineView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 08/03/2019.
//

import Cocoa;
import ChronicyFramework;

@IBDesignable
class TimelineView: NSScrollView {
    
    private var stacks: [TimelineStackView] = [];
    
    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setupView();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setupView();
    }

}

extension TimelineView {
    private func setupView() {
        self.addSubview(TimelineStackView(frame: NSRect(x: 0, y: 0, width: 800, height: 600)));
    }
}
