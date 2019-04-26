//
//  Activity.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 24/04/2019.
//

import Foundation;

public class Activity {
    public var name: String;
    public var dateStarted: Date;
    public var dateFinished: Date?;
    
    public init(name: String, dateStarted: Date, dateFinished: Date) {
        self.name = name;
        self.dateStarted = dateStarted;
        self.dateFinished = dateFinished;
    }
    
//    public func duration(in: Date) -> TimeInterval? {
//        guard let finished: Date = self.dateFinished else {
//            return nil;
//        }
//        
//    }
}

extension Activity: Equatable {
    public static func == (lhs: Activity, rhs: Activity) -> Bool {
        return lhs.name == rhs.name &&
            lhs.dateStarted == rhs.dateStarted &&
            lhs.dateFinished == rhs.dateFinished;
    }
}
