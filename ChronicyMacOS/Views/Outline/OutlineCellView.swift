//
//  TimelineCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

@IBDesignable
class OutlineCellView: NSTableCellView {

    @IBOutlet private weak var titleLabel: NSTextField!;
    @IBOutlet private weak var dateLabel: NSTextField!;
    @IBOutlet private weak var subtitleLabel: NSTextField!;
    @IBOutlet private weak var colorImageView: NSImageView!;
    
    public var cellIndex: Int = 0;
    public weak var parent: OutlineStackView?;
    
    public var delegate: OutlineCellViewDelegate?;
    public var interactionDelegate: ViewInteractionDelegate?;
    
    public var editTrigger: OutlineViewController.ActionTrigger? = .click;
    public var deleteTrigger: OutlineViewController.ActionTrigger?;
    
    @IBInspectable
    public var title: String = String();
    
    @IBInspectable
    public var subtitle: String = String();
    
    @IBInspectable
    public var date: Date = Date();
    
    private lazy var dateFormatter: DateFormatter = {
        let formatter: DateFormatter = DateFormatter();
        formatter.timeZone = TimeZone.autoupdatingCurrent;
        formatter.dateStyle = .short;
        formatter.timeStyle = .short;
        
        return formatter;
    } ()
        
    override func draw(_ dirtyRect: NSRect) {
        super.draw(dirtyRect);
        
        self.layer?.shadowOpacity = self.shadowOpacity;
        self.layer?.shadowColor = self.shadowColor.cgColor;
        self.layer?.shadowOffset = self.shadowOffset;
        self.layer?.shadowRadius = self.shadowRadius;
        self.layer?.cornerRadius = self.cornerRadius;
    }
    
    override func mouseDown(with event: NSEvent) {
        super.mouseUp(with: event);
        self.interactionDelegate?.onClick(at: event.locationInWindow, in: self);
        
        if self.editTrigger == .click {
            self.onEdit();
        }
    }

    override func rightMouseDown(with event: NSEvent) {
        super.rightMouseUp(with: event);
        self.interactionDelegate?.onRightClick(at: event.locationInWindow, in: self);
        
        if self.editTrigger == .rightClick {
            self.onEdit();
        }
    }
}

extension OutlineCellView: CustomOperationSeparatable {
    func onLoadData() {
        
    }
    
    func onLayoutView() {
        self.setupFonts();
//        self.colorImageView.image = NSImage(filledWith: NSColor.red);
        
        self.titleLabel.stringValue = self.title;
        self.subtitleLabel.stringValue = self.subtitle;
        self.dateLabel.stringValue = dateFormatter.string(from: self.date);
    }
}

extension OutlineCellView {
    private func setupFonts() {
        let titleFont: NSFont? = NSFont(name: "JosefinSans-Regular", size: 25.0);
        self.titleLabel.font = titleFont;
        
        let subtitleFont: NSFont? = NSFont(name: "JosefinSans-Regular", size: 13.0);
        self.subtitleLabel.font = subtitleFont;
        
        let dateFont: NSFont? = NSFont(name: "JosefinSans-Regular", size: 13.0);
        self.dateLabel.font = dateFont;
    }
    
    @objc
    private func onEdit() {
        self.delegate?.onEdit(cellView: self);
    }
    
    @objc
    private func onDelete() {
        self.delegate?.onDelete(cellView: self);
    }
}

protocol OutlineCellViewDelegate {
    func onEdit(cellView: OutlineCellView);
    func onDelete(cellView: OutlineCellView);
}
