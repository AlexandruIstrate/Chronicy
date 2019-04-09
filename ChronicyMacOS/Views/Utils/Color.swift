//
//  Color.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 03/04/2019.
//

import Cocoa;

extension NSImage {
    public convenience init(filledWith color: NSColor) {
        self.init();
        
        let image: CGImage = self.cgImage as! CGImage;

        let width: CGFloat = self.size.width;
        let height: CGFloat = self.size.height;
        let bounds: CGRect = CGRect(x: 0, y: 0, width: width, height: height);

        let colorSpace: CGColorSpace = CGColorSpaceCreateDeviceRGB();
        let bitmapInfo: CGBitmapInfo = CGBitmapInfo(rawValue: CGImageAlphaInfo.premultipliedLast.rawValue);

        let context: CGContext = CGContext(data: nil, width: Int(width), height: Int(height), bitsPerComponent: 8, bytesPerRow: 0, space: colorSpace, bitmapInfo: bitmapInfo.rawValue)!;
        context.clip(to: bounds, mask: image);
        context.setFillColor(color.cgColor);
        context.fill(bounds);

        let contextImage: CGImage = context.makeImage()!;
        
        self.backgroundColor = color;
    }
    
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
