//
//  BrowserTracker.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;
import ChronicyFramework;

typealias BrowserVersion = (major: Int, minor: Int, patch: Int);

enum Browser {
    case safari;
    case chrome;
    case firefox;
    case other(name: String);
}

protocol BrowserModule: Module {
    func browserName() -> Browser;
    func browserVersion() -> BrowserVersion;
    
    func launch(/* with: BrowserLaunchParams */);
    
    func loadExtension(/* with: ExtensionLoadParams */);
    func unloadExtension();
}

class SafariBrowserModule: BrowserModule {
    
    func browserName() -> Browser {
        return .safari;
    }
    
    func browserVersion() -> BrowserVersion {
        return (0, 0, 0);
    }
    
    func launch() {
        Log.info(message: "Launching Safari...");
        
        Log.info(message: "Succesfuly launched Safari!");
    }
    
    func loadExtension() {
        Log.info(message: "Loading Safari extension...");
        
        Log.info(message: "Succesfuly loaded Safari extension!");
    }
    
    func unloadExtension() {
        Log.info(message: "Unloading Safari extension...");
        
        Log.info(message: "Succesfuly unloaded Safari extension!");
    }
    
    func moduleName() -> String {
        return "SafariBrowserModule";
    }
    
    func priority() -> ModulePriority {
        return .low;    // We don't want to slow down browsing
    }
    
    func update() {
        // TODO: Add
    }
    
}

extension SafariBrowserModule {
    class OpenPagesProperty: TrackableProperty {
        typealias T = [URL];
        
        func hasUpdates() -> Bool {
            return false;
        }
        
        func newData() -> T {
            return [];
        }
    }
}
