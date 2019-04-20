//
//  NotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public protocol NotebookManager {
    typealias NotebookManagerInfoCallback = (NotebookInfo?, NotebookManagerError?) -> ();
    func getInfo(info: NotebookInfo, callback: @escaping NotebookManagerInfoCallback);
    
    typealias NotebookManagerInfoAllCallback = ([NotebookInfo]?, NotebookManagerError?) -> ();
    func getInfo(callback: @escaping NotebookManagerInfoAllCallback);
    
    typealias NotebookManagerNotebookCallback = (Notebook?, NotebookManagerError?) -> ();
    func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback);
}

public struct NotebookInfo {
    public var name: String;
    public var id: String;
    public var dateCreated: Date;
    
    public init(name: String, id: String, dateCreated: Date) {
        self.name = name;
        self.id = id;
        self.dateCreated = dateCreated;
    }
}

public enum NotebookManagerError: Error {
    case fetchFailure;
}
