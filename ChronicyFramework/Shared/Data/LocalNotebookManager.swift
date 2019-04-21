//
//  LocalNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;
import CoreData;

public class LocalNotebookManager: NotebookManager {
    public func getInfo(info: NotebookInfo, callback: @escaping NotebookManagerInfoCallback) {
        
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoAllCallback) {
        
    }
    
    public func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback) {
        
    }
}

class CoreDataNotebook: NSManagedObject {
    
    public var notebook: Notebook?;
    
    @NSManaged public var name: String;
}

class CoreDataStack: NSManagedObject {
    
}

class CoreDataCard: NSManagedObject {
    
}
