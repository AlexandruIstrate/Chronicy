//
//  PreferencesWindowController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;
import Cocoa;

public class PreferencesWindowController: NSWindowController {
    
    public static var shared: PreferencesWindowController?;
    
    public override init(window: NSWindow?) {
        super.init(window: window);
        PreferencesWindowController.shared = self;
    }
    
    public required init?(coder: NSCoder) {
        super.init(coder: coder);
        PreferencesWindowController.shared = self;
    }
}
