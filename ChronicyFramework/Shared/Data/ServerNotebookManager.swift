//
//  ServerNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class ServerNotebookManager: NotebookManager {
    
    private var dataSource: ServerNotebookDataSource!;
    private lazy var api: WebAPI = {
        return WebAPI();
    } ();
    
    public init() {
        self.dataSource = ServerNotebookDataSource(api: api);
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoAllCallback) {
        
    }
    
    public func getInfo(id: String, callback: @escaping NotebookManagerInfoCallback) {
        
    }
    
    public func dataSource(info: NotebookInfo) -> NotebookDataSource {
        return dataSource;
    }
}
