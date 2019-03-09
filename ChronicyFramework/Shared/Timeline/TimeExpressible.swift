//
//  TimeExpressable.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

public protocol TimeExpressible {
    associatedtype T;
    
    func older(than date: Date) -> T;
    func newer(than date: Date) -> T;
}
