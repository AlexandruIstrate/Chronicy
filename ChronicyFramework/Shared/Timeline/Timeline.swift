//
//  Timeline+CoreDataClass.swift
//  
//
//  Created by Alexandru Istrate on 25/03/2019.
//
//

import Foundation;
import CoreData;

public class Timeline: NSManagedObject {
    
    public var tasksArray: [Task] {
        return Array(self.tasks.sorted(by: { (first: Task, second: Task) -> Bool in
            return (first.date.compare(second.date as Date) == .orderedAscending);
        }))
    }
    
    public func add(task: Task) {
        tasks.append(task);
        
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.tasksModified.rawValue), object: self, userInfo: nil);
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: Notifications.taskAdded.rawValue), object: self, userInfo: nil);
        
        self.addToTasks(task);
    }
    
    public func remove(task: Task) {
        self.removeFromTasks(task);
    }
    
    
    @discardableResult
    public func insertNewTask() -> Task {
        let task: Task = NSEntityDescription.insertNewObject(forEntityName: "Task", into: CoreDataStack.stack.managedObjectContext) as! Task;
        self.tasks.insert(task);
        
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

extension Timeline {
    
    @nonobjc public class func fetchRequest() -> NSFetchRequest<Timeline> {
        return NSFetchRequest<Timeline>(entityName: "Timeline");
    }
    
    @NSManaged public var name: String;
    @NSManaged public var tasks: Set<Task>;
    
}

extension Timeline {
    
    @objc(addTasksObject:)
    @NSManaged public func addToTasks(_ value: Task)
    
    @objc(removeTasksObject:)
    @NSManaged public func removeFromTasks(_ value: Task)
    
    @objc(addTasks:)
    @NSManaged public func addToTasks(_ values: NSSet)
    
    @objc(removeTasks:)
    @NSManaged public func removeFromTasks(_ values: NSSet)
    
}

extension Timeline {
    public enum Notifications: String, NotificationName {
        case tasksModified;
        case taskAdded;
        case taskRemoved;
    }
}
