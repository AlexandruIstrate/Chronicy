//
//  CustomField.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public protocol CustomField {
    
    var value: Any { get set }
    
    func onAdd(to card: Card);
    func onRemove(from card: Card);
    
    func onDraw(rect: CGRect);
    func onPress(at: CGPoint);
    func onHover();
    func onDrag();
}
