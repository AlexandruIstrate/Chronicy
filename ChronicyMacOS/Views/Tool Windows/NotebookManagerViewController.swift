//
//  NotebookManagerViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 02/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class NotebookManagerViewController: NSViewController {
    
    @IBOutlet private weak var tableView: NSTableView!;
    
    public var notebooks: [Notebook] = [];
    
    enum Title: String {
        case name = "Name";
        case stacks = "Stacks";
        
        public var identifier: NSUserInterfaceItemIdentifier {
            return NSUserInterfaceItemIdentifier(self.rawValue);
        }
    }

    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupTable();
    }
    
    private func setupTable() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
    
}

extension NotebookManagerViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return self.notebooks.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        let cell: NSTableCellView? = nil;
        
        let notebook: Notebook = self.notebooks[row];
        
        switch tableColumn?.title {
        case Title.name.rawValue:
            guard let cell: NSTableCellView = tableView.makeView(withIdentifier: Title.name.identifier, owner: self) as? NSTableCellView else {
                break;
            }
            
            cell.textField?.stringValue = notebook.name;
            
        case Title.stacks.rawValue:
            guard let cell: NSTableCellView = tableView.makeView(withIdentifier: Title.stacks.identifier, owner: self) as? NSTableCellView else {
                break;
            }
            
            cell.textField?.stringValue = notebook.stacks.map({ (iter: Stack) -> String in
                return iter.name;
            }).joined(separator: ", ");
            
        default:
            Log.error(message: "Could not determine column type!");
            break;
        }
        
        return cell;
    }
}
