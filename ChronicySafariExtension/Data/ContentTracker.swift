//
//  ContentTracker.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;

protocol ContentTracker {
    func trackerType() -> ContentTrackerType;
    func sendMessage(message: String);
}

enum ContentTrackerType {
    case url;
}

class URLTracker: ContentTracker {
    func trackerType() -> ContentTrackerType {
        return .url;
    }
    
    func sendMessage(message: String) {
        NSLog("Message recieved: \(message)");
    }
}
