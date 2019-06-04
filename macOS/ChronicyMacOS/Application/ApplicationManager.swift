//
//  ApplicationManager.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class MacOSApplicationLauncher: ApplicationLauncher {
    func launch(for bundleID: String) throws {
        guard let path: String = NSWorkspace.shared.absolutePathForApplication(withBundleIdentifier: bundleID) else {
            throw ApplicationLauncherError.notFound;
        }
        
        NSWorkspace.shared.launchApplication(path);
    }
    
    func launch(path: String) throws {
        do {
            try NSWorkspace.shared.launchApplication(at: URL(fileURLWithPath: path), options: .default, configuration: [:]);
        } catch {
            throw ApplicationLauncherError.notFound;
        }
    }
    
    func launch(defaultApplication: DefaultApplication) throws {
        try self.launch(for: defaultApplication.rawValue);
    }
}
