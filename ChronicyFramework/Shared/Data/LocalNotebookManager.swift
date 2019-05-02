//
//  LocalNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;

public class LocalNotebookManager: NotebookManager {
    
    private var storage: CoreDataStorage = CoreDataStorage();
    
    // TODO: Testing only: Remove
    private static let notebook: Notebook = {
        var ret: Notebook = Notebook(name: "Main");
        
        let card1: Card = Card(title: "Card 1");
        let card2: Card = Card(title: "Card 2");
        let card3: Card = Card(title: "Card 3");
        
        let stack1: Stack = Stack(name: "Stack 1");
        stack1.add(card: card1);
        stack1.add(card: card2);
        stack1.add(card: card3);
        
        let otherCard1: Card = Card(title: "Card 1");
        let otherCard2: Card = Card(title: "Card 2");
        
        let stack2: Stack = Stack(name: "Stack 2");
        stack2.add(card: otherCard1);
        stack2.add(card: otherCard2);
        
        ret.add(stack: stack1);
        ret.add(stack: stack2);
        
        return ret;
    } ();
    
    public init() {
        
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoCallback) {
        
    }
    
    public func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback) {
        let fetchRequest: NSFetchRequest<CoreDataNotebook> = CoreDataNotebook.fetch();
        
        do {
            let notebooks: [CoreDataNotebook] = try fetchRequest.execute();
            callback(notebooks[0].notebook, nil);
        } catch let e {
            Log.error(message: "Could not fetch notebook: \(e.localizedDescription)");
        }
    }
    
    public func saveNotebook(notebook: Notebook) {
        
    }
}
