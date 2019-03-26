//
//  TimelineManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Foundation;
import CoreData;

public class TimelineManager {
    
    public static let manager: TimelineManager = TimelineManager(timelineName: "Main");
    
    public let timeline: Timeline;
    private let coreDataStack: CoreDataStack = CoreDataStack.stack;
    
    private init(timelineName: String) {
        // TODO: Clean this up!!!
        let request: NSFetchRequest<Timeline> = Timeline.fetchRequest();
        
        do {
            let items: [Timeline]? = try CoreDataStack.stack.managedObjectContext?.fetch(request);
            
            guard let timelines: [Timeline] = items else {
                throw NSError(domain: "App", code: 322, userInfo: nil);
            }
            
            guard let value: Timeline = timelines.first else {
                throw NSError(domain: "App", code: 323, userInfo: nil);
            }
            
            self.timeline = value;
        } catch let e {
            let item: Timeline = NSEntityDescription.insertNewObject(forEntityName: "Timeline", into: coreDataStack.managedObjectContext!) as! Timeline;
            item.name = timelineName;
            self.timeline = item;
            
            Log.error(message: "An error occured while fetching Timeline from CoreData: \(e.localizedDescription)");
        }
        
        self.registerTaskObservers();
    }
    
    deinit {
        self.deregisterTaskObservers();
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

extension TimelineManager {
    private func registerTaskObservers() {
        let nc: NotificationCenter = NotificationCenter.default;
        nc.addObserver(self, selector: #selector(onTasksModified), name: NSNotification.Name(rawValue: Timeline.Notifications.tasksModified.rawValue), object: nil);
    }
    
    private func deregisterTaskObservers() {
        let nc: NotificationCenter = NotificationCenter.default;
        nc.removeObserver(self, name: NSNotification.Name(rawValue: Timeline.Notifications.tasksModified.rawValue), object: nil);
    }
    
    @objc
    private func onTasksModified(notification: Notification) {
        let taskNames: [String] = TimelineManager.manager.timeline.tasks.map { (task: Task) -> String in
            return task.name;
        }
        DistributedObjectManager.manager.set(object: taskNames, for: SharedConstants.DistributedObjectKeys.tasks);
    }
}
