//
//  TrackingService.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 06/04/2019.
//

import Foundation;

public class TrackingService {
    
}

public class TrackedObject {
    
    public var name: String;
    public private(set) var history: [TrackedAction] = [];
    
    public init(name: String) {
        self.name = name;
    }
}

public struct TrackedAction {
    var dateOccured: Date;
    var description: String;
    var data: [String : Any];
}
