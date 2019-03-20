//
//  ViewBuilder.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Cocoa;

protocol FromXibLoadable {
    static func fromNib<T: NSView>(nibName: String, with bundle: Bundle) -> T?;
}

extension NSView: FromXibLoadable {
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

// TODO: Remove
extension NSView {
    func bindFrameToSuperviewBounds() {
        guard let superview = self.superview else {
            print("Error! `superview` was nil – call `addSubview(view: UIView)` before calling `bindFrameToSuperviewBounds()` to fix this.")
            return
        }
        
        self.translatesAutoresizingMaskIntoConstraints = false
        self.topAnchor.constraint(equalTo: superview.topAnchor, constant: 0).isActive = true
        self.bottomAnchor.constraint(equalTo: superview.bottomAnchor, constant: 0).isActive = true
        self.leadingAnchor.constraint(equalTo: superview.leadingAnchor, constant: 0).isActive = true
        self.trailingAnchor.constraint(equalTo: superview.trailingAnchor, constant: 0).isActive = true
        
    }
}
