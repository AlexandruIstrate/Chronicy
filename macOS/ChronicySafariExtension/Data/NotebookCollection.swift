//
//  NotebookCollection.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 27/04/2019.
//

import Foundation;

class NotebookCollection {
    
    public static var collection: NotebookCollection = NotebookCollection();
    
    public var notebooks: [ExtensionNotebook] = [];
    
    private init() {}
    
    public func add(notebook: ExtensionNotebook) {
        self.notebooks.append(notebook);
    }
    
    public func remove(notebook: ExtensionNotebook) {
        self.notebooks.removeAll { (iter: ExtensionNotebook) -> Bool in
            return iter == notebook;
        }
    }
    
    public func named(name: String) -> ExtensionNotebook? {
        return self.notebooks.first(where: { (notebook: ExtensionNotebook) -> Bool in
            return notebook.name == name;
        })
    }
    
    public func load() {        
        guard let newNotebooks: [ExtensionNotebook] = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.notebooks, action: .keepUnchanged) else {
            Log.info(message: "No notebooks available!");
            return;
        }
        
        self.notebooks = newNotebooks;
    }
}

