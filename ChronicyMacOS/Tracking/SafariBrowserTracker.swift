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
        
        do {
            try ApplicationManager.manager.launch(defaultApplication: .safari);
        } catch let e {
            Log.error(message: "Could not launch Safari! Reason: \(e.localizedDescription)");
            return;
        }
        
        Log.info(message: "Succesfuly launched Safari!");
    }
    
    override func onLoad() {
        setupProperties();
    }
    
    override func moduleName() -> String {
        return "SafariBrowserModule";
    }
    
    override func priority() -> ModulePriority {
        return .low;    // We don't want to slow down browsing
    }    
}

extension SafariBrowserModule {
    class CurrentPageProperty: TrackableProperty {
        
        public var url: URL?;
        
        func onRefreshData() {
            self.url = DistributedObjectManager.manager.get(for: "currentPageURL");
            Log.info(message: "URL is \(String(describing: url))");
        }
    }
}

extension SafariBrowserModule {
    private func setupProperties() {
        self.properties = [ currentPage ];
    }
}
