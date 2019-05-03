//
//  LocalNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;
import CoreData;

public class LocalNotebookManager: NotebookManager {
    
    private var storage: CoreDataStorage = CoreDataStorage();
    private var context: NSManagedObjectContext {
        return self.storage.managedObjectContext!;
    }
    
//    // TODO: Testing only: Remove
//    private static let notebook: Notebook = {
//        var ret: Notebook = Notebook(name: "Main");
//
//        let card1: Card = Card(title: "Card 1");
//        let card2: Card = Card(title: "Card 2");
//        let card3: Card = Card(title: "Card 3");
//
//        let stack1: Stack = Stack(name: "Stack 1");
//        stack1.add(card: card1);
//        stack1.add(card: card2);
//        stack1.add(card: card3);
//
//        let otherCard1: Card = Card(title: "Card 1");
//        let otherCard2: Card = Card(title: "Card 2");
//
//        let stack2: Stack = Stack(name: "Stack 2");
//        stack2.add(card: otherCard1);
//        stack2.add(card: otherCard2);
//
//        ret.add(stack: stack1);
//        ret.add(stack: stack2);
//
//        return ret;
//    } ();
    
    public init() {
        
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoCallback) {
        guard let notebooks: [CoreDataNotebook] = self.getNotebooks() else {
            Log.error(message: "Could not retrieve notebooks!");
            callback(nil, .fetchFailure);
            return;
        }
        
        let info: [NotebookInfo] = notebooks.map { (iter: CoreDataNotebook) -> NotebookInfo in
            return NotebookInfo(name: iter.name, id: iter.name, dateCreated: Date());
        }
        
        callback(info, nil);
    }
    
    public func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback) {
        guard let notebooks: [CoreDataNotebook] = self.getNotebooks() else {
            Log.error(message: "Could not retrieve notebooks!");
            callback(nil, .fetchFailure);
            return;
        }
        
        let filtered: [CoreDataNotebook] = notebooks.filter { (iter: CoreDataNotebook) -> Bool in
            // TODO: Compare the other fields
            return iter.name == info.name;
        }
        
        guard filtered.count == 1 else {
            callback(nil, .itemNotFound);
            return;
        }
        
        callback(filtered.first!.notebook, nil);
    }
    
    public func saveNotebook(notebook: Notebook) {
        // TODO: Check if this notebook exists already before insertinga new one. If it exists, then use that one
        
        guard let item: CoreDataNotebook = NSEntityDescription.insertNewObject(forEntityName: "CoreDataNotebook", into: self.context) as? CoreDataNotebook else {
            Log.error(message: "Could not create Core Data Notebook");
            return;
        }
        item.notebook = notebook;
    }
    
    private func getNotebooks() -> [CoreDataNotebook]? {
        let fetchRequest: NSFetchRequest<CoreDataNotebook> = CoreDataNotebook.fetch();
        
        do {
            guard let notebooks: [CoreDataNotebook] = try self.storage.managedObjectContext?.fetch(fetchRequest) else {
                Log.error(message: "Could not retrieve notebooks!");
                return nil;
            }
            
            return notebooks;
        } catch let e {
            Log.error(message: "Could not fetch notebooks: \(e.localizedDescription)");
        }
        
        return nil;
    }
}
