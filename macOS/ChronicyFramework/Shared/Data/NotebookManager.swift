//
//  NotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public protocol NotebookManager {
    typealias NotebookManagerInfoCallback = ([NotebookInfo]?, NotebookManagerError?) -> ();
    func getInfo(callback: @escaping NotebookManagerInfoCallback);
    
    typealias NotebookManagerNotebookCallback = (Notebook?, NotebookManagerError?) -> ();
    func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback);
    
    typealias NotebookManagerNotebooksCallback = ([Notebook]?, NotebookManagerError?) -> ();
    func retrieveAllNotebooks(callback: @escaping NotebookManagerNotebooksCallback);
    
    func saveNotebook(notebook: Notebook) throws;
}

extension NotebookManager {
    public typealias NotebookManagerNamedCallback = (Notebook?, NotebookManagerError?) -> ();
    public func named(name: String, callback: @escaping NotebookManagerNamedCallback) {
        self.getInfo { (info: [NotebookInfo]?, error: NotebookManagerError?) in
            guard let info: [NotebookInfo] = info else {
                callback(nil, error);
                return;
            }
            
            guard let itemInfo: NotebookInfo = info.first(where: { (iter: NotebookInfo) -> Bool in
                return iter.name == name;
            }) else {
                callback(nil, .itemNotFound);
                return;
            }
            
            self.retrieveNotebook(info: itemInfo, callback: { (notebook: Notebook?, error: NotebookManagerError?) in
                guard let notebook: Notebook = notebook else {
                    callback(nil, error);
                    return;
                }
                
                callback(notebook, nil);
            })
        }
    }
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
    case itemNotFound;
}
