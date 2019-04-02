//
//  Notebook.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 01/04/2019.
//

import Foundation;

public protocol NotebookItem {
    func onAdd(to notebook: Notebook);
    func onRemove(from notebook: Notebook);
}

public class Notebook {
    
    public private(set) var items: [Stack] = [];
    
    public init() {
        
    }
    
    public func add(stack: Stack) {
        self.items.append(stack);
    }
    
    public func remove(stack: Stack) {
        self.items.removeAll { (iter: Stack) -> Bool in
            return iter == stack;
        }
    }
}
