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
        if let titleValue: String = self.titleValue {
            self.titleLabel.stringValue = titleValue;
//            Log.warining(message: "Title value not set");
        }
        
        if let commentValue: String = self.commentValue {
            self.commentLabel.stringValue = commentValue;
//            Log.warining(message: "Comment value not set");
        }
        
        if let dateValue: Date = self.dateValue {
            let formatter: DateFormatter = DateFormatter();
            formatter.dateFormat = "HH";
            self.dateLabel.stringValue = formatter.string(from: dateValue);
//            Log.warining(message: "Date value not set");
        }
    }
}
