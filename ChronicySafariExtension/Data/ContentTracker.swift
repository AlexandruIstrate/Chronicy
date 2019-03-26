//
//  ContentTracker.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;
import ChronicyFramework;

protocol ContentTracker {
    func trackerType() -> ContentTrackerType;
    func sendData(data: Any);
}

enum ContentTrackerType {
    case url;
}

class URLTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .url;
    }
    
    func sendData(data: Any) {
        guard let url: URL = data as? URL else {
            fatalError("Not compatible with URL!");
        }
        
        DistributedObjectManager.manager.set(object: url, for: "currentPageURL");
    }
}
