//
//  CoreDataTypes.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;

protocol CoreDataObject: NSManagedObject {
    static func fetch() -> NSFetchRequest<Self>;
}

final class CoreDataShelf: NSManagedObject, CoreDataObject {
    @NSManaged var notebooks: Set<CoreDataNotebook>;
    
    @nonobjc static func fetch() -> NSFetchRequest<CoreDataShelf> {
        return NSFetchRequest<CoreDataShelf>(entityName: "CoreDataShelf");
    }
}

final class CoreDataNotebook: NSManagedObject, CoreDataObject {
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
            // TODO: The rest
        }
    }
    
    @NSManaged var name: String;
    @NSManaged var stacks: Set<CoreDataStack>;
    
    @NSManaged var shelf: CoreDataShelf?;
    
    static func fetch() -> NSFetchRequest<CoreDataNotebook> {
        fatalError("Not implemented!");
    }
}

final class CoreDataStack: NSManagedObject, CoreDataObject {
    var stack: Stack {
        let ret: Stack = Stack(name: self.name);
        ret.cards = Array(self.cards).map({ (iter: CoreDataCard) -> Card in
            return iter.card;
        })
        return ret;
    }
    
    @NSManaged var name: String;
    @NSManaged var cards: Set<CoreDataCard>;
    
    @NSManaged var notebook: CoreDataNotebook?;
    
    static func fetch() -> NSFetchRequest<CoreDataStack> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCard: NSManagedObject, CoreDataObject {
    var card: Card {
        let ret: Card = Card(title: self.title, date: self.date);
        ret.fields = Array(self.fields).map({ (iter: CoreDataCustomField) -> CustomField? in
            return iter.field;
        }).filter({ (iter: CustomField?) -> Bool in
            return iter != nil;
        }).map({ (iter: CustomField?) -> CustomField in
            return iter!;
        });
        return ret;
    }
    
    @NSManaged var title: String;
    @NSManaged var date: Date;
    
    @NSManaged var fields: [CoreDataCustomField];
    @NSManaged var tags: Set<CoreDataCardTag>;
    
    @NSManaged var stack: CoreDataStack?;
    
    static func fetch() -> NSFetchRequest<CoreDataCard> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCustomField: NSManagedObject, CoreDataObject {
    var field: CustomField? {
        guard let actualType: FieldType = FieldType(rawValue: self.type) else {
            Log.error(message: "Could not convert field type name to type");
            return nil;
        }
        
        var ret: CustomField = TextField.instantiate(by: actualType, name: self.name);
//        ret.displayName = self.displayName;
        ret.value = self.value
        return ret;
    }
    
    @NSManaged var name: String;
    @NSManaged var displayName: String;
    @NSManaged var value: Any?;
    @NSManaged var type: String;
    
    @NSManaged var card: CoreDataCard?;
    
    static func fetch() -> NSFetchRequest<CoreDataCustomField> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCardTag: NSManagedObject, CoreDataObject {
    var tag: CardTag?;
    
    @NSManaged var title: String;
    @NSManaged var color: CGColor;
    
    @NSManaged var card: CoreDataCard?;
    
    static func fetch() -> NSFetchRequest<CoreDataCardTag> {
        fatalError("Not implemented!");
    }
}
