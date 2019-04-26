//
//  CoreDataTypes.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;

protocol CoreDataObject: NSManagedObject {
    func fetchRequest() -> NSFetchRequest<Self>;
}

final class CoreDataNotebook: NSManagedObject, CoreDataObject {
    var notebook: Notebook?;
    
    @NSManaged var name: String;
    @NSManaged var stacks: Set<CoreDataStack>;
    
    func fetchRequest() -> NSFetchRequest<CoreDataNotebook> {
        fatalError("Not implemented!");
    }
}

final class CoreDataStack: NSManagedObject, CoreDataObject {
    var stack: Stack?;
    
    @NSManaged var name: String;
    @NSManaged var cards: Set<CoreDataCard>;
    
    @NSManaged var notebook: CoreDataNotebook?;
    
    func fetchRequest() -> NSFetchRequest<CoreDataStack> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCard: NSManagedObject, CoreDataObject {
    var card: Card?;
    
    @NSManaged var title: String;
    @NSManaged var date: Date;
    
    @NSManaged var fields: [CoreDataCustomField];
    @NSManaged var tags: Set<CoreDataCardTag>;
    
    @NSManaged var stack: CoreDataStack?;
    
    func fetchRequest() -> NSFetchRequest<CoreDataCard> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCustomField: NSManagedObject, CoreDataObject {
    var field: CustomField?;
    
    @NSManaged var name: String;
    @NSManaged var displayName: String;
    @NSManaged var value: Any?;
    @NSManaged var type: String;
    
    @NSManaged var card: CoreDataCard?;
    
    func fetchRequest() -> NSFetchRequest<CoreDataCustomField> {
        fatalError("Not implemented!");
    }
}

final class CoreDataCardTag: NSManagedObject, CoreDataObject {
    var tag: CardTag?;
    
    @NSManaged var title: String;
    @NSManaged var color: CGColor;
    
    @NSManaged var card: CoreDataCard?;
    
    func fetchRequest() -> NSFetchRequest<CoreDataCardTag> {
        fatalError("Not implemented!");
    }
}
