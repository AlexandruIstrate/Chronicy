//
//  ApplicationManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;

public protocol ApplicationLauncher {
    func launch(for bundleID: String) throws;
    func launch(defaultApplication: DefaultApplication) throws;
}

public enum ApplicationLauncherError: Error {
    case notFound;
}

public enum DefaultApplication: String {
    case safari = "com.apple.safari";
    case terminal = "com.apple.terminal";
}

public class ApplicationManager {
    
    public static let manager: ApplicationManager = ApplicationManager();
    
    private let queue: DispatchQueue = DispatchQueue(label: "ApplicationManagerDispatchQueue");
    public var launcher: ApplicationLauncher!;
    
    public func launch(for bundleID: String) throws {
        try queue.sync {
            do {
                try self.launcher.launch(for: bundleID);
            } catch let e as ApplicationLauncherError {
                Log.error(message: "Could not open application \(bundleID): \(e.localizedDescription).");
                throw e;
            }
        }
    }
    
    public func launch(defaultApplication: DefaultApplication) throws {
        try queue.sync {
            do {
                try self.launcher.launch(defaultApplication: defaultApplication);
            } catch let e as ApplicationLauncherError {
                Log.error(message: "Could not open application \(defaultApplication.rawValue): \(e.localizedDescription).");
                throw e;
            }
        }
    }
}
