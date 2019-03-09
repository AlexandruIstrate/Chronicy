//
//  TimelineManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

public class TimelineManager {
    
    public let timeline: Timeline;
    
    public init(timelineName: String) {
        self.timeline = Timeline(name: timelineName);
    }
    
    public init(with data: Data) {
        // TODO: Implement
        self.timeline = Timeline(name: "Test");
    }
    
    public func data() -> Data {
        // TODO: Implement
        return Data(capacity: 0);
    }
}
