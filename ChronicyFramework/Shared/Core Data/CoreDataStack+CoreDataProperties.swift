//
//  CoreDataStack+CoreDataProperties.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/05/2019.
//
//

import Foundation
import CoreData


extension CoreDataStack {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<CoreDataStack> {
        return NSFetchRequest<CoreDataStack>(entityName: "CoreDataStack");
    }

    @NSManaged public var name: String;
    @NSManaged public var cards: Set<CoreDataCard>;
    @NSManaged public var inputFields: Set<CoreDataCustomField>;
    @NSManaged public var notebook: CoreDataNotebook?
    
    var stack: Stack {
        get {
            let ret: Stack = Stack(name: self.name);
            ret.cards = Array(self.cards).map({ (iter: CoreDataCard) -> Card in
                return iter.card;
            });
            ret.inputTemplate.register(replace: self.inputFields.map({ (iter: CoreDataCustomField) -> CustomField? in
                return iter.field;
            }).filter({ (iter: CustomField?) -> Bool in
                return iter != nil;
            }).map({ (iter: CustomField?) -> CustomField in
                return iter!;
            }));
            return ret;
        }
        set {
            self.name = newValue.name;
            
            for card: Card in newValue.cards {
                guard let c: CoreDataCard = card.card(insert: CoreDataStorage.defaultContext) else {
                    Log.error(message: "Could not create card!");
                    continue;
                }
                
                self.addToCards(c);
            }
        }
    }

}

// MARK: Generated accessors for cards
extension CoreDataStack {

    // Cards
    
    @objc(addCardsObject:)
    @NSManaged public func addToCards(_ value: CoreDataCard)

    @objc(removeCardsObject:)
    @NSManaged public func removeFromCards(_ value: CoreDataCard)

    @objc(addCards:)
    @NSManaged public func addToCards(_ values: NSSet)

    @objc(removeCards:)
    @NSManaged public func removeFromCards(_ values: NSSet)
    
    // Input Fields
    
    @objc(addInputFieldsObject:)
    @NSManaged public func addToInputFields(_ value: CoreDataCustomField)
    
    @objc(removeInputFieldsObject:)
    @NSManaged public func removeFromInputFields(_ value: CoreDataCustomField)
    
    @objc(addInputFields:)
    @NSManaged public func addToInputFields(_ values: NSSet)
    
    @objc(removeInputFields:)
    @NSManaged public func removeFromInputFields(_ values: NSSet)

}

extension Stack {
    public func stack(insert into: NSManagedObjectContext) -> CoreDataStack? {
        let s: CoreDataStack? = NSEntityDescription.insertNewObject(forEntityName: "CoreDataStack", into: into) as? CoreDataStack;
        s?.name = self.name;
        s?.inputFields = Set(self.inputTemplate.fields.map({ (iter: CustomField) -> CoreDataCustomField? in
            return iter.field(insert: into);
        }).filter({ (iter: CoreDataCustomField?) -> Bool in
            return iter != nil;
        }).map({ (iter: CoreDataCustomField?) -> CoreDataCustomField in
            return iter!;
        }));
        
        for card: Card in self.cards {
            guard let c: CoreDataCard = card.card(insert: CoreDataStorage.defaultContext) else {
                Log.error(message: "Could not create card!");
                continue;
            }
            
            s?.addToCards(c);
        }
        
        return s;
    }
}
