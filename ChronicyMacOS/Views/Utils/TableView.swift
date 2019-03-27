//
//  TableView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 27/03/2019.
//

import Cocoa;

extension NSTableView {
    public func makeCell(identifier: String) -> NSTableCellView? {
        guard let cell: NSTableCellView = self.makeView(withIdentifier: NSUserInterfaceItemIdentifier(rawValue: identifier), owner: self) as? NSTableCellView else {
            return nil;
        }
        
        return cell;
    }
}
