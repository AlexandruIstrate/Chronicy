//
//  ActivityViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/04/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ActivityViewController: NSViewController {

    @IBOutlet private weak var tableView: NSTableView!;
    
    @IBOutlet private weak var timePopUp: NSPopUpButton!;
    @IBOutlet private weak var sortCriteriaPopUp: NSPopUpButton!;
    
    private var activityManager: ActivityManager = ActivityManager.manager;
    
    enum ColumnName: String {
        case title = "Title";
        case comment = "Comment";
        case date = "Date";
    }
    
    enum CellIdentifier: String {
        case title = "TitleCell";
        case comment = "CommentCell";
        case date = "DateCell";
        
        var identifier: NSUserInterfaceItemIdentifier {
            return NSUserInterfaceItemIdentifier(rawValue: self.rawValue);
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setup();
    }
    
}

extension ActivityViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return activityManager.activities.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {        
        let activity: Activity = self.activityManager.activities[row];
        
        switch tableColumn!.title {
        case ColumnName.title.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: CellIdentifier.title.identifier, owner: self) as? NSTableCellView else {
                break;
            }
            view.textField?.stringValue = activity.name;
            return view;
        case ColumnName.comment.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: CellIdentifier.title.identifier, owner: self) as? NSTableCellView else {
                break;
            }
            view.textField?.stringValue = activity.comment;
            return view;
        case ColumnName.date.rawValue:
            guard let view: NSTableCellView = tableView.makeView(withIdentifier: CellIdentifier.title.identifier, owner: self) as? NSTableCellView else {
                break;
            }
            let formatter: DateFormatter = DateFormatter();
            formatter.dateFormat = "MMM d, HH:mm:ss";
            view.textField?.stringValue = formatter.string(from: activity.date);
            return view;
        default:
            break;
        }
        
        return nil;
    }
}

extension ActivityViewController {
    private func setup() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
}
