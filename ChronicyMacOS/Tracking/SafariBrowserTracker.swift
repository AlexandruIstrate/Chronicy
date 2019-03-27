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
            
            guard let urlString: String = self.url?.absoluteString else {
                Log.error(message: "Could not get string from URL!");
                return;
            }
            
            guard let taskName: String = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.browserSelectedTask, action: .keepUnchanged) else {
                Log.error(message: "Could not get task name!");
                return;
            }
            
//            TimelineManager.manager.timeline.task(forName: taskName)?.add(action: )
            guard let action: Action = TimelineManager.manager.timeline.task(forName: taskName)?.insertNewAction() else {
                Log.error(message: "Could not get action for task name!");
                return;
            }
            
            action.name = urlString;
            Log.info(message: "Added action with data: \(urlString)");
            
            let im: InteractionsManager = InteractionsManager.manager;
            im.register(trigger: URLRecievedTrigger.key()) { (key: URLRecievedTrigger.Key) in
                
            };
            im.raise(trigger: URLRecievedTrigger.key());
        }
    }
}

extension SafariBrowserModule {
    private func setupProperties() {
        self.properties = [ currentPage ];
    }
}
