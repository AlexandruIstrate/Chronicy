//
//  ActivityCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/04/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ActivityCellView: NSTableCellView {
    @IBOutlet private weak var titleLabel: NSTextField! { didSet { self.setup(); } }
    @IBOutlet private weak var dateLabel: NSTextField! { didSet { self.setup(); } }
    @IBOutlet private weak var commentLabel: NSTextField! { didSet { self.setup(); } }
    
    public var titleValue: String?;
    public var commentValue: String?;
    public var dateValue: Date?;
    
    private func setup() {
        guard let titleValue: String = self.titleValue else {
            Log.warining(message: "Title value not set");
            return;
        }
        
        self.titleLabel.stringValue = titleValue;
        
        guard let commentValue: String = self.commentValue else {
            Log.warining(message: "Comment value not set");
            return;
        }
        
        self.commentLabel.stringValue = commentValue;
        
        guard let dateValue: Date = self.dateValue else {
            Log.warining(message: "Date value not set");
            return;
        }
        
        let formatter: DateFormatter = DateFormatter();
        formatter.dateFormat = "HH";
        self.dateLabel.stringValue = formatter.string(from: dateValue);
    }
}
