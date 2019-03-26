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
    
    private lazy var documentsDirectory: URL = {
        let urls: [URL] = FileManager.default.urls(for: .documentDirectory, in: .userDomainMask);
        
        let endIndex: Int = urls.index(before: urls.endIndex);
        return urls[endIndex];
    } ();
    
    private lazy var managedObjectModel: NSManagedObjectModel? = {
        guard let modelURL: URL = Bundle(for: type(of: self)).url(forResource: "Timeline", withExtension: "momd") else {
            Log.error(message: "Failed retrieving URL for model file!");
            return nil;
        }
        
        return NSManagedObjectModel(contentsOf: modelURL);
    } ();
    
    private lazy var persistentStoreCoordinator: NSPersistentStoreCoordinator? = {
        guard let model: NSManagedObjectModel = self.managedObjectModel else {
            return nil;
        }
        
        let coordinator: NSPersistentStoreCoordinator = NSPersistentStoreCoordinator(managedObjectModel: model);
        let url: URL = self.documentsDirectory.appendingPathComponent("Timeline.sqlite");
        
        do {
            try coordinator.addPersistentStore(ofType: NSSQLiteStoreType, configurationName: nil, at: url, options: nil);
        } catch {
            Log.error(message: "Failed adding SQLite persistent store!");
            return nil;
        }
        
        return coordinator;
    } ();
    
    public lazy var managedObjectContext: NSManagedObjectContext? = {
        guard let coordinator: NSPersistentStoreCoordinator = self.persistentStoreCoordinator else {
            return nil;
        }
        
        let context: NSManagedObjectContext = NSManagedObjectContext(concurrencyType: .mainQueueConcurrencyType);
        context.persistentStoreCoordinator = coordinator;
        
        return context;
    } ();
}
