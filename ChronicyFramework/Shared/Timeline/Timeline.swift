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
        
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.tasksModified.rawValue), object: self, userInfo: nil);
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.taskAdded.rawValue), object: self, userInfo: nil);
    }
    
    @discardableResult
    public func insertNewTask() -> Task {
        let task: Task = Task(name: "New Task");
        self.add(task: task);
        
        return task;
    }
    
    public func remove(task: Task) {
        tasks.removeAll { (iter: Task) -> Bool in
            return iter == task;
        }
        
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.tasksModified.rawValue), object: self, userInfo: nil);
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.taskRemoved.rawValue), object: self, userInfo: nil);
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

extension Timeline {
    public enum Notifications: String, NotificationName {
        case tasksModified;
        case taskAdded;
        case taskRemoved;
    }
}
