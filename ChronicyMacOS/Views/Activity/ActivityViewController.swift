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
    
    private var activityManager: ActivityManager = ActivityManager();
    
    override func viewDidLoad() {
        self.activityManager.add(activity: Activity(name: "Name", comment: "Comment", date: Date()));
        super.viewDidLoad();
        self.setup();
    }
    
}

extension ActivityViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return activityManager.activities.count;
    }
    
    func tableView(_ tableView: NSTableView, viewFor tableColumn: NSTableColumn?, row: Int) -> NSView? {
        guard let view: ActivityCellView = ActivityCellView.fromNib() else {
            return nil;
        }
        
        let activity: Activity = self.activityManager.activities[row];
        view.titleValue = activity.name;
        view.commentValue = activity.comment;
        view.dateValue = activity.date;
        return view;
    }
}

extension ActivityViewController {
    private func setup() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
}
