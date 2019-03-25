//
//  CoreDataStack.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 23/03/2019.
//

import CoreData;

public class CoreDataStack {
    
    public static let stack: CoreDataStack = CoreDataStack();
    
    private init() {}
    
    lazy var managedObjectContext: NSManagedObjectContext = {
        return self.persistentContainer.viewContext;
    } ();
    
    private lazy var persistentContainer: NSPersistentContainer = {
        let fileName: String = "Timeline";
        
        guard let modelURL: URL = Bundle(for: type(of: self)).url(forResource: fileName, withExtension: "momd") else {
            fatalError("Error loading model from bundle");
        }
        
        guard let mom: NSManagedObjectModel = NSManagedObjectModel(contentsOf: modelURL) else {
            fatalError("Error initializing mom from: \(modelURL)");
        }
        
        let container: NSPersistentContainer = NSPersistentContainer(name: fileName, managedObjectModel: mom);
        container.loadPersistentStores() { (description: NSPersistentStoreDescription, error: Error?) in
            guard let error: Error = error else {
                return;
            }

            Log.fatal(message: "Could not create CoreData container! Reason: \(error)");
        }
                
        return container;
    } ()
    
}
