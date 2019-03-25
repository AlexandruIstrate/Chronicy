//
//  SafariBrowserTracker.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 17/03/2019.
//

import Foundation;
import ChronicyFramework;

class SafariBrowserModule: BrowserModule {
    
    public var currentPage: CurrentPageProperty = CurrentPageProperty();
    
    override func browserName() -> Browser {
        return .safari;
    }
    
    override func browserVersion() -> BrowserVersion {
        return (0, 0, 0);
    }
    
    override func launch() {
        Log.info(message: "Launching Safari...");
        
        setupProperties();
        
        Log.info(message: "Succesfuly launched Safari!");
    }
    
    override func loadExtension() {
        Log.info(message: "Loading Safari extension...");
        
        Log.info(message: "Succesfuly loaded Safari extension!");
    }
    
    override func unloadExtension() {
        Log.info(message: "Unloading Safari extension...");
        
        Log.info(message: "Succesfuly unloaded Safari extension!");
    }
    
    override func moduleName() -> String {
        return "SafariBrowserModule";
    }
    
    override func priority() -> ModulePriority {
        return .low;    // We don't want to slow down browsing
    }
    
    override func update() {
        super.update();
//        Log.info(message: "SafariBrowserModule Updated!");
    }
    
}

extension SafariBrowserModule {
    class CurrentPageProperty: TrackableProperty {
        
        public var url: URL?;
        
        func onRefreshData() {
            self.url = DistributedObjectManager.manager.get(for: "currentPageURL");
        }
    }
}

extension SafariBrowserModule {
    private func setupProperties() {
        self.properties = [ currentPage ];
    }
}
