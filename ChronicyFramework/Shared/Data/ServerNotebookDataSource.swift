//
//  ServerNotebookDataSource.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class ServerNotebookDataSource: NotebookDataSource {
    
    private var api: WebAPI;
    
    public init(api: WebAPI) {
        self.api = api;
    }
    
    public func retrieveNotebook() throws -> Notebook {
        // TODO: Implement
        throw NotebookDataSourceError.unavailable;
    }
}
