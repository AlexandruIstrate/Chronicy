//
//  StateAdapter.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 14/03/2019.
//

import Foundation;

class StateAdapter: NSObject {
    
    public var state: ExtensionStateManager.State;
    
    @objc public dynamic var stateString: String {
        get { return state.rawValue; }
        set { if let conv: ExtensionStateManager.State = ExtensionStateManager.State(rawValue: newValue) { self.state = conv; } }
    }
    
    init(state: ExtensionStateManager.State) {
        self.state = state;
    }
    
    init?(string: String) {
        guard let state: ExtensionStateManager.State = ExtensionStateManager.State(rawValue: string) else {
            return nil;
        }
        
        self.state = state;
    }
}
