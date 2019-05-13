//
//  Activity.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 24/04/2019.
//

import Foundation;

public class Activity {
    public var name: String;
    public var comment: String;
    public var date: Date;
    
    public init(name: String, comment: String, date: Date) {
        self.name = name;
        self.comment = comment;
        self.date = date;
    }
}

extension Activity: Equatable {
    public static func == (lhs: Activity, rhs: Activity) -> Bool {
        return  lhs.name == rhs.name &&
                lhs.comment == rhs.comment &&
                lhs.date == rhs.date;
    }
}

public class ActivityManager {
    public private(set) var activities: [Activity] = [];
    
    public init() {
        
    }
    
    public func add(activity: Activity) {
        self.activities.append(activity);
    }
    
    public func remove(activity: Activity) {
        self.activities.removeAll { (iter: Activity) -> Bool in
            return iter == activity;
        }
    }
}
