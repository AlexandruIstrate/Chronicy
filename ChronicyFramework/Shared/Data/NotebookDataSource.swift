//
//  NotebookDataSource.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 13/04/2019.
//

import Foundation;

public protocol NotebookDataSource {
    func retrieveNotebook() throws -> Notebook;
}

public enum NotebookDataSourceError: Error {
    case unavailable;
}
