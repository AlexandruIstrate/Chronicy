//
//  TimelineCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;
import ChronicyFramework;

@IBDesignable
class OutlineCellView: NSTableCellView {

    @IBOutlet private weak var titleLabel: NSTextField!;
    @IBOutlet private weak var dateLabel: NSTextField!;
    @IBOutlet private weak var subtitleLabel: NSTextField!;
    @IBOutlet private weak var iconImageView: NSImageView!;
    
    public var cellIndex: Int = 0;
    public weak var parent: OutlineStackView?;
    
    public var delegate: OutlineCellViewDelegate?;
    public var interactionDelegate: ViewInteractionDelegate?;
    
    @IBInspectable
    public var title: String = String();
    
    @IBInspectable
    public var subtitle: String = String();
    
    @IBInspectable
    public var date: Date = Date();
    
//    @IBInspectable
//    public var dateFormat: String = "MMM d, h:mm a" {
//        didSet {
//            self.dateFormatter.dateFormat = self.dateFormat;
//        }
//    }
    
//    @IBInspectable
//    public var icon: NSImage = NSImage(named: NSImage.Name("More"))!;
    
    @IBInspectable
    public var background: NSColor = NSColor.white {
        didSet {
            self.layer?.backgroundColor = self.background.cgColor;
        }
    }
    
    @IBInspectable
    public var shadowOpacity: Float = 0.0 {
        didSet {
            self.layer?.shadowOpacity = self.shadowOpacity;
        }
    }
    
    @IBInspectable
    public var shadowColor: NSColor = NSColor.white {
        didSet {
            self.layer?.shadowColor = self.shadowColor.cgColor;
        }
    }
    
    @IBInspectable
    public var shadowOffset: CGSize = CGSize.zero {
        didSet {
            self.layer?.shadowOffset = self.shadowOffset;
        }
    }
    
    @IBInspectable
    public var shadowRadius: CGFloat = 0.0 {
        didSet {
            self.layer?.shadowRadius = self.shadowRadius;
        }
    }
    
    @IBInspectable
    public var cornerRadius: CGFloat = 0.0 {
        didSet {
            self.layer?.cornerRadius = self.cornerRadius;
        }
    }
    
    private lazy var dateFormatter: DateFormatter = {
        let formatter: DateFormatter = DateFormatter();
        formatter.timeZone = TimeZone.autoupdatingCurrent;
        formatter.dateStyle = .short;
        formatter.timeStyle = .short;
//        formatter.dateFormat = "MMM d, h:mm a";
        
        return formatter;
    } ()
    
    override init(frame frameRect: NSRect) {
        super.init(frame: frameRect);
        setupView();
    }
    
    required init?(coder decoder: NSCoder) {
        super.init(coder: decoder);
        setupView();
    }
    
    override func draw(_ dirtyRect: NSRect) {
        super.draw(dirtyRect);
        
        self.layer?.shadowOpacity = self.shadowOpacity;
        self.layer?.shadowColor = self.shadowColor.cgColor;
        self.layer?.shadowOffset = self.shadowOffset;
        self.layer?.shadowRadius = self.shadowRadius;
        self.layer?.cornerRadius = self.cornerRadius;
    }
    
    override func mouseUp(with event: NSEvent) {
        super.mouseUp(with: event);
        self.interactionDelegate?.onRightClick(at: event.locationInWindow);
    }

    override func rightMouseUp(with event: NSEvent) {
        super.rightMouseUp(with: event);
        self.interactionDelegate?.onRightClick(at: event.locationInWindow);
    }
}

extension OutlineCellView: CustomOperationSeparatable {
    func onLoadData() {
        
    }
    
    func onLayoutView() {
        self.setupFonts();
        
        self.titleLabel.stringValue = self.title;
        self.subtitleLabel.stringValue = self.subtitle;
        self.dateLabel.stringValue = dateFormatter.string(from: self.date);
    }
}

extension OutlineCellView {
    private func setupView() {
        self.wantsLayer = true;
        
        self.background = NSColor(calibratedRed: 255 / 255.0, green: 255 / 255.0, blue: 255 / 255.0, alpha: 1);
        self.cornerRadius = 10.0;
    }
    
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
