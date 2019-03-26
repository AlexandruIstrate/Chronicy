//
//  Action+CoreDataClass.swift
//  
//
//  Created by Alexandru Istrate on 25/03/2019.
//
//

import Foundation
import CoreData

public class Action: NSManagedObject {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<Action> {
        return NSFetchRequest<Action>(entityName: "Action")
    }
    
    @NSManaged public var comment: String;
    @NSManaged public var date: Date;
    @NSManaged public var name: String;
    
    @NSManaged public weak var task: Task?;

}
