//
//  Timeline.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;

public class Timeline {
    public var name: String;
    private var internalTasks: [Task] = [];
    
    public var tasks: [Task] { return self.internalTasks; }
    
    public init(name: String) {
        self.name = name;
    }
}

extension Timeline: TimeExpressible {
    public typealias T = [Task];
    
    public func older(than date: Date) -> T {
        return internalTasks.filter({ (task: Task) -> Bool in
            return task.date < date;
        });
    }
    
    public func newer(than date: Date) -> T {
        return internalTasks.filter({ (task: Task) -> Bool in
            return task.date > date;
        });
    }
}
