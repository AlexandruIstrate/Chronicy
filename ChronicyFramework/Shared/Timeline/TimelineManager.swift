//
//  TimelineManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;
import CoreData;

public class TimelineManager {
    
    private let coreDataStack: CoreDataStack = CoreDataStack.stack;
    
    public let timeline: Timeline;
    
    public init(timelineName: String) {
        let item: NSManagedObject = NSEntityDescription.insertNewObject(forEntityName: "Timeline", into: coreDataStack.managedObjectContext);
        self.timeline = item as! Timeline;
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
