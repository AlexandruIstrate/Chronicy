//
//  ExtensionStateManager.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;

class ExtensionStateManager {
    enum State: String {
        case enabled = "Enabled";
        case disabled = "Disabled";
    }
    
    enum PerformanceMode {
        case background;
        case realtime;
    }
    
    public static let manager: ExtensionStateManager = ExtensionStateManager();
    
    public var state: State = .disabled { didSet { onInternalStateChange(); } }
    public var performanceMode: PerformanceMode = .background { didSet { onInternalPerformanceModeChange(); } }
    
    private init() {}
}

extension ExtensionStateManager {
    private func onInternalStateChange() {
        
    }
    
    private func onInternalPerformanceModeChange() {
        
    }
}
