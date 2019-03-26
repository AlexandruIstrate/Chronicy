//
//  Task.swift
//  
//
//  Created by Alexandru Istrate on 25/03/2019.
//
//

import Foundation;
import CoreData;

public class Task: NSManagedObject {
    
    public var actionsArray: [Action] {
        return Array(self.actions.sorted(by: { (first: Action, second: Action) -> Bool in
            return (first.date.compare(second.date as Date) == .orderedAscending);
        }))
    }

    public func add(action: Action) {
        self.addToActions(action);
    }
    
    public func remove(action: Action) {
        self.removeFromActions(action);
    }
    
    @discardableResult
    public func insertNewAction() -> Action {
        let action: Action = NSEntityDescription.insertNewObject(forEntityName: "Action", into: CoreDataStack.stack.managedObjectContext!) as! Action;
        action.name = "New Action";
        action.comment = "";
        action.date = Date();
        self.add(action: action);
        
        return action;
    }

}

extension Task {
    
    @nonobjc public class func fetchRequest() -> NSFetchRequest<Task> {
        return NSFetchRequest<Task>(entityName: "Task")
    }
    
    @NSManaged public var comment: String;
    @NSManaged public var date: Date;
    @NSManaged public var name: String;
    @NSManaged public var actions: Set<Action>;
    
    @NSManaged public weak var timeline: Timeline?;
    
}

extension Task {
    
    @objc(addActionsObject:)
    @NSManaged public func addToActions(_ value: Action)
    
    @objc(removeActionsObject:)
    @NSManaged public func removeFromActions(_ value: Action)
    
    @objc(addActions:)
    @NSManaged public func addToActions(_ values: NSSet)
    
    @objc(removeActions:)
    @NSManaged public func removeFromActions(_ values: NSSet)
    
}
