//
//  NotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public protocol NotebookManager {
    typealias NotebookManagerInfoAllCallback = ([NotebookInfo]?, NotebookManagerError?) -> ();
    func getInfo(callback: @escaping NotebookManagerInfoAllCallback);
    
    typealias NotebookManagerInfoCallback = (NotebookInfo?, NotebookManagerError?) -> ();
    func getInfo(id: String, callback: @escaping NotebookManagerInfoCallback);
    
    func dataSource(info: NotebookInfo) -> NotebookDataSource;
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
