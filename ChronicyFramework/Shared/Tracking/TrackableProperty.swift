//
//  TrackableProperty.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public protocol TrackableProperty {
    associatedtype T;
    
    func hasUpdates() -> Bool;
    func newData() -> T;
}
