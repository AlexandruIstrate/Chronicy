//
//  CoreDataCard+CoreDataProperties.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/05/2019.
//
//

import Foundation
import CoreData


extension CoreDataCard {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<CoreDataCard> {
        return NSFetchRequest<CoreDataCard>(entityName: "CoreDataCard");
    }

    @NSManaged public var comment: String;
    @NSManaged public var date: NSDate;
    @NSManaged public var name: String;
    @NSManaged public var stack: CoreDataStack?
    @NSManaged public var tags: Set<CoreDataCardTag>;
    @NSManaged public var fields: Set<CoreDataCustomField>;
    
    var card: Card {
        get {
            let ret: Card = Card(title: self.name, comment: self.comment, date: self.date as Date);
            ret.fields = Array(self.fields).map({ (iter: CoreDataCustomField) -> CustomField? in
                return iter.field;
            }).filter({ (iter: CustomField?) -> Bool in
                return iter != nil;
            }).map({ (iter: CustomField?) -> CustomField in
                return iter!;
            });
            return ret;
        }
        set {
            self.date = newValue.date as NSDate;
            self.name = newValue.name;
            self.comment = newValue.comment;
            
            for field: CustomField in newValue.fields {
                guard let f: CoreDataCustomField = field.field(insert: CoreDataStorage.defaultContext) else {
                    Log.error(message: "Could not create field!");
                    continue;
                }
                
                self.addToFields(f);
            }
        }
    }

}

// MARK: Generated accessors for tags
extension CoreDataCard {

    @objc(addTagsObject:)
    @NSManaged public func addToTags(_ value: CoreDataCardTag)

    @objc(removeTagsObject:)
    @NSManaged public func removeFromTags(_ value: CoreDataCardTag)

    @objc(addTags:)
    @NSManaged public func addToTags(_ values: NSSet)

    @objc(removeTags:)
    @NSManaged public func removeFromTags(_ values: NSSet)

}

// MARK: Generated accessors for fields
extension CoreDataCard {

    @objc(addFieldsObject:)
    @NSManaged public func addToFields(_ value: CoreDataCustomField)

    @objc(removeFieldsObject:)
    @NSManaged public func removeFromFields(_ value: CoreDataCustomField)

    @objc(addFields:)
    @NSManaged public func addToFields(_ values: NSSet)

    @objc(removeFields:)
    @NSManaged public func removeFromFields(_ values: NSSet)

}

extension Card {
    public func card(insert into: NSManagedObjectContext) -> CoreDataCard? {
        let c: CoreDataCard? = NSEntityDescription.insertNewObject(forEntityName: "CoreDataCard", into: into) as? CoreDataCard;
        c?.date = self.date as NSDate;
        c?.name = self.name;
        c?.comment = self.comment;
        
        for field: CustomField in self.fields {
            guard let f: CoreDataCustomField = field.field(insert: CoreDataStorage.defaultContext) else {
                Log.error(message: "Could not create field!");
                continue;
            }
            
            c?.addToFields(f);
        }
        
        return c;
    }
}
