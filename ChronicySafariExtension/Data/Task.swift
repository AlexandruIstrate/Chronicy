//
//  Task.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 23/03/2019.
//

import Foundation;
import ChronicyFramework;

struct Task: Equatable {
    var name: String;
}

class TaskManager {
    
    public static var manager: TaskManager = TaskManager();
    
    public var tasks: [Task] = [];
    
    private init() {}
    
    public func add(task: Task) {
        self.tasks.append(task);
    }
    
    public func remove(task: Task) {
        self.tasks.removeAll { (iter: Task) -> Bool in
            return iter == task;
        }
    }
    
    public func load() {
        guard let tasks: [String] = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.tasks, action: .none) else {
            return;
        }
        
        for taskName: String in tasks {
            self.tasks.append(Task(name: taskName));
        }
    }
    
}
