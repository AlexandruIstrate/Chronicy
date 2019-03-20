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
}

extension OutlineCellView: CustomOperationSeparatable {
    func onLoadData() {
        
    }
    
    func onLayoutView() {
        
    }
}
