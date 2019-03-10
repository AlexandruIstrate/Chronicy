//
//  ViewBuilder.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Cocoa;

extension NSView {
    public static func fromNib<T: NSView>(nibName: String = String(describing: T.self), with bundle: Bundle = Bundle.main) -> T? {
        var topLevelArray: NSArray?;
        bundle.loadNibNamed(NSNib.Name(nibName), owner: self, topLevelObjects: &topLevelArray);
        
        guard let results: NSArray = topLevelArray else {
            return nil;
        }
        
        let views: [Any] = Array<Any>(results).filter { $0 is T };
        return views.last as? T;
    }
}
