//
//  CardTag.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/04/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

extension CardTag {
    public var nsColor: NSColor {
        return NSColor(cgColor: self.color) ?? NSColor.white;
    }
}
