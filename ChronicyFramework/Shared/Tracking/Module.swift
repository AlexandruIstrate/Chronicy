//
//  Module.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public protocol Module {
    func moduleName() -> String;
    func priority() -> ModulePriority;
    func update();
}

public enum ModulePriority {
    case low;
    case medium;
    case high;
}
