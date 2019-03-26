//
//  TimelineManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;

// TODO: Load from CoreData
public class TimelineManager {
    
    public static let manager: TimelineManager = TimelineManager(name: "Main");
    
    public let timeline: Timeline;
    
    private init(name: String) {
        self.timeline = Timeline(name: name);
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
