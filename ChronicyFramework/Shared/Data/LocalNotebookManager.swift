//
//  LocalNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 22/04/2019.
//

import Foundation;

public class LocalNotebookManager: NotebookManager {
    
    private var storage: CoreDataStorage = CoreDataStorage();
    
    public init() {
        
    }
    
    public func getInfo(info: NotebookInfo, callback: @escaping NotebookManagerInfoCallback) {
        
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoAllCallback) {
        
    }
    
    public func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback) {
        
    }
}
