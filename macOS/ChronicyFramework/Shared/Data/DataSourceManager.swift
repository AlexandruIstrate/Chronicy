//
//  DataSourceManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 10/07/2019.
//

import Foundation;

public class DataSourceManager {
    
    public static let manager: DataSourceManager = DataSourceManager();
    
    private var _sourceType: NotebookManagerType = .local;
    public var sourceType: NotebookManagerType
    {
        get { return _sourceType; }
        set {
            _sourceType = newValue;
            
            for sub: SourceChangedDelegate in subscribers {
                sub.changed(type: newValue);
            }
        }
    }
    
    public var subscribers: [SourceChangedDelegate] = [];
    
    public func subscribe(item: SourceChangedDelegate) {
        self.subscribers.append(item);
    }
    
}

public protocol SourceChangedDelegate {
    func changed(type: NotebookManagerType);
}
