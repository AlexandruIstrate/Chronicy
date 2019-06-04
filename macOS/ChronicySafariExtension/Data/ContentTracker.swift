//
//  ContentTracker.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;

protocol ContentTracker {
    func trackerType() -> ContentTrackerType;
    func sendData(data: Any);
}

enum ContentTrackerType {
    case url;
    case stack;
    case notebook;
}

class URLTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .url;
    }
    
    func sendData(data: Any) {
        guard let url: URL = data as? URL else {
            Log.error(message: "Not compatible with URL!");
            return;
        }
        
        DistributedObjectManager.manager.set(object: url, for: SharedConstants.DistributedObjectKeys.pageURLData);
    }
}

class NotebookTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .notebook;
    }
    
    func sendData(data: Any) {
        guard let notebookName: String = data as? String else {
            Log.error(message: "Not compatible with notebook!");
            return;
        }
        
        DistributedObjectManager.manager.set(object: notebookName, for: SharedConstants.DistributedObjectKeys.browserSelectedNotebook);
    }
}

class StackTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .stack;
    }
    
    func sendData(data: Any) {
        guard let stackName: String = data as? String else {
            Log.error(message: "Not compatible with stack!");
            return;
        }
        
        DistributedObjectManager.manager.set(object: stackName, for: SharedConstants.DistributedObjectKeys.browserSelectedStack);
    }
}
