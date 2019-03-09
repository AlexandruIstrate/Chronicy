//
//  Trackable.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

protocol Trackable {
    func onStart();
    func onDeactivate();
    func onTerminate();
}
