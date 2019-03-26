//
//  ContentTrackerManager.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 13/03/2019.
//

import Foundation;

class ContentTrackerManager {
    
    public static let manager: ContentTrackerManager = ContentTrackerManager();
    
    private var trackers: [ContentTrackerType : ContentTracker] = [:];
    
    private init() {
        self.registerTrackers();
    }
    
    public func add(tracker: ContentTracker) {
        self.trackers[tracker.trackerType()] = tracker;
    }
    
    public func remove(tracker: ContentTracker) {
        self.trackers.removeValue(forKey: tracker.trackerType());
    }
    
    public func sendData(data: Any, trackerType: ContentTrackerType? = nil) {
        let matchingTrackers: [ContentTracker]!;
        
        if let trackerType: ContentTrackerType = trackerType {
            let tracker: ContentTracker? = self.trackers[trackerType];
            matchingTrackers = (tracker != nil ? [tracker!] : []);
        } else {
            matchingTrackers = Array(self.trackers.values);
        }
        
        for tracker: ContentTracker in matchingTrackers {
            tracker.sendData(data: data);
        }
    }
    
    public subscript(trackerType: ContentTrackerType) -> ContentTracker? {
        get { return self.trackers[trackerType]; }
        set { self.trackers[trackerType] = newValue; }
    }
}

extension ContentTrackerManager {
    private func registerTrackers() {
        self.add(tracker: URLTracker());
    }
}
