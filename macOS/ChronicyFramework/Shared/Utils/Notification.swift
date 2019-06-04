//
//  Notification.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/03/2019.
//

import Foundation;

public protocol NotificationName {
    var name: Notification.Name { get }
}

extension RawRepresentable where RawValue == String, Self: NotificationName {
    public var name: Notification.Name {
        get {
            return Notification.Name(rawValue: self.rawValue);
        }
    }
}
