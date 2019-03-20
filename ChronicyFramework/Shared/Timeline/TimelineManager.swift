//
//  TimelineManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

// TODO: Load from CoreData
public class TimelineManager {
    
    public let timeline: Timeline;
    
    public init(timelineName: String) {
        self.timeline = Timeline(name: timelineName);
    }
    
}
