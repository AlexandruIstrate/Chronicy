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
    
    public func add(task: Task) {
        self.timeline.add(task: task);
    }
    
    public func add(action: Action, task: Task) -> Bool {
        let tasks: [Task] = self.timeline.tasks.filter { (iter: Task) -> Bool in
            return iter == task;
        }
        
        guard let task: Task = tasks.first else {
            return false;
        }
        
        task.add(action: action);
        
        return true;
    }
    
    public func add(action: Action, taskName: String) -> Bool {
        let tasks: [Task] = self.timeline.tasks.filter { (iter: Task) -> Bool in
            return iter.name == taskName;
        }
        
        guard let task: Task = tasks.first else {
            return false;
        }
        
        task.add(action: action);
        
        return true;
    }
    
}
