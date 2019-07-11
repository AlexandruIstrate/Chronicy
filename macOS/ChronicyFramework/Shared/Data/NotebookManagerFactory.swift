//
//  NotebookManagerFactory.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/07/2019.
//

import Foundation;

public class NotebookManagerFactory {
    public static func create(type: NotebookManagerType) -> NotebookManager {
        switch type {
        case .local:
            return LocalNotebookManager();
        case .web:
            return ServerNotebookManager();
        }
    }
}

public enum NotebookManagerType {
    case local;
    case web;
}
