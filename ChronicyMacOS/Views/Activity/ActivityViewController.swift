//
//  ActivityViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/04/2019.
//

import Cocoa;

class ActivityViewController: NSViewController {

    @IBOutlet private weak var tableView: NSTableView!;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setup();
    }
    
}

extension ActivityViewController: NSTableViewDataSource, NSTableViewDelegate {
    func numberOfRows(in tableView: NSTableView) -> Int {
        return 0;
    }
    
    
}

extension ActivityViewController {
    private func setup() {
        self.tableView.dataSource = self;
        self.tableView.delegate = self;
    }
}
