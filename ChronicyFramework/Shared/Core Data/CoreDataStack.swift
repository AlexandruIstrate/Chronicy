//
//  CoreDataStack.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 23/03/2019.
//

import CoreData;

public class CoreDataStack {
    
    private lazy var persistentContainer: NSPersistentContainer = {
        let container: NSPersistentContainer = NSPersistentContainer(name: "Timeline");
        container.loadPersistentStores() { (description: NSPersistentStoreDescription, error: Error?) in
//            return nil;
        }
        
        return container;
    } ()
    
}
