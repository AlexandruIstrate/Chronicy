//
//  CoreDataNotebook+CoreDataProperties.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/05/2019.
//
//

import Foundation;
import CoreData;

extension CoreDataNotebook {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<CoreDataNotebook> {
        return NSFetchRequest<CoreDataNotebook>(entityName: "CoreDataNotebook")
    }

    @NSManaged public var name: String;
    @NSManaged public var stacks: Set<CoreDataStack>;
    
    var notebook: Notebook {
        get {
            let ret: Notebook = Notebook(name: self.name);
            ret.stacks = Array(self.stacks).map({ (iter: CoreDataStack) -> Stack in
                return iter.stack;
            });
            return ret;
        }
        set {
            self.name = newValue.name;
            self.stacks.removeAll();
            
            for stack: Stack in newValue.stacks {
                guard let s: CoreDataStack = stack.stack(insert: CoreDataStorage.defaultContext) else {
                    Log.error(message: "Could not create stack!");
                    continue;
                }
                
                self.addToStacks(s);
            }
        }
    }

}

// MARK: Generated accessors for stacks
extension CoreDataNotebook {

    @objc(addStacksObject:)
    @NSManaged public func addToStacks(_ value: CoreDataStack)

    @objc(removeStacksObject:)
    @NSManaged public func removeFromStacks(_ value: CoreDataStack)

    @objc(addStacks:)
    @NSManaged public func addToStacks(_ values: NSSet)

    @objc(removeStacks:)
    @NSManaged public func removeFromStacks(_ values: NSSet)

}

extension Notebook {
    public func notebook(insert into: NSManagedObjectContext) -> CoreDataNotebook? {
        let nb: CoreDataNotebook? = NSEntityDescription.insertNewObject(forEntityName: "CoreDataNotebook", into: into) as? CoreDataNotebook;
        nb?.name = self.name;
        
        for stack: Stack in self.stacks {
            guard let s: CoreDataStack = stack.stack(insert: CoreDataStorage.defaultContext) else {
                Log.error(message: "Could not create stack!");
                continue;
            }
            
            nb?.addToStacks(s);
        }
        
        return nb;
    }
}
