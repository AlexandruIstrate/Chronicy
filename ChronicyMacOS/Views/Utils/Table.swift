//
//  Table.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 06/05/2019.
//

import Cocoa;

extension NSTableView {
    public var selectedRowIndex: Int? {
        let ret: Int = self.selectedRow;
        
        guard ret >= 0 else {
            return nil;
        }
        
        return ret;
    }
    
    public var selectedColumnIndex: Int? {
        let ret: Int = self.selectedColumn;
        
        guard ret >= 0 else {
            return nil;
        }
        
        return ret;
    }
}
