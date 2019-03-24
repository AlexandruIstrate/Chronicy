//
//  Timeline.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;

public class Timeline {
    public private(set) var name: String;
    public private(set) var tasks: [Task] = [];
    
    public init(name: String) {
        self.name = name;
    }
    
    public func add(task: Task) {
        tasks.append(task);
    }
    
    @discardableResult
    public func insertNewTask() -> Task {
        let task: Task = Task(name: "New Task");
        self.tasks.append(task);
        
        return task;
    }
    
    public func remove(task: Task) {
        tasks.removeAll { (iter: Task) -> Bool in
            return iter == task;
        }
    }
}

extension Timeline: TimeExpressible {
    public typealias T = [Task];
    
    public func older(than date: Date) -> T {
        return tasks.filter({ (task: Task) -> Bool in
            return task.date < date;
        });
    }
    
    public func newer(than date: Date) -> T {
        return tasks.filter({ (task: Task) -> Bool in
            return task.date > date;
        });
    }
}
