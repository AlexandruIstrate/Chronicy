//
//  Content.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFramework;

class Content: NSObject {
    
    public var timelineManager: TimelineManager!;
    
    init(name: String) {
        self.timelineManager = TimelineManager(timelineName: name);
    }
    
}

extension Content {
    
    func read(from data: Data) {
        self.timelineManager = TimelineManager(with: data);
    }
    
    func data() -> Data? {
        return self.timelineManager.data();
    }
    
}
