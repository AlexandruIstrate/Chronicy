//
//  CoreDataCardTag+CoreDataProperties.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/05/2019.
//
//

import Foundation
import CoreData


extension CoreDataCardTag {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<CoreDataCardTag> {
        return NSFetchRequest<CoreDataCardTag>(entityName: "CoreDataCardTag")
    }

    @NSManaged public var color: NSObject;
    @NSManaged public var name: String;
    @NSManaged public var card: CoreDataCard?

}
