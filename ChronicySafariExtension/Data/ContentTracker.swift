//
//  ContentTracker.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;

protocol ContentTracker {
    func trackerType() -> ContentTrackerType;
    func sendMessage(message: Any);
}

enum ContentTrackerType {
    case url;
}

class URLTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .url;
    }
    
    func sendMessage(message: Any) {
        guard let url: URL = message as? URL else {
            fatalError("Not compatible with URL!");
        }
        
        DistributedObjectManager.manager.set(object: url, for: "currentPageURL");
    }
}
