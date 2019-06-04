//
//  Color.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 03/04/2019.
//

import Cocoa;

extension NSImage {    
    public func image(tintColor: NSColor) -> NSImage {
        if self.isTemplate == false {
            return self;
        }
        
        let image: NSImage = self.copy() as! NSImage;
        image.lockFocus();
        
        tintColor.set();
        NSMakeRect(0, 0, image.size.width, image.size.height).fill(using: NSCompositingOperation.sourceAtop);
        
        image.unlockFocus();
        image.isTemplate = false;
        
        return image;
    }
}
