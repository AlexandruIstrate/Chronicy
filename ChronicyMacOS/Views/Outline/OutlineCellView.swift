//
//  TimelineCellView.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 09/03/2019.
//

import Cocoa;

@IBDesignable
class OutlineCellView: NSView {

    @IBOutlet private weak var titleLabel: NSTextField!;
    @IBOutlet private weak var dateLabel: NSTextField!;
    @IBOutlet private weak var subtitleLabel: NSTextField!;
    @IBOutlet private weak var iconImageView: NSImageView!;
    
    @IBInspectable
    public var title: String = String() {
        didSet {
            self.titleLabel.stringValue = self.title;
        }
    }
    
    @IBInspectable
    public var subtitle: String = String() {
        didSet {
            self.subtitleLabel.stringValue = self.subtitle;
        }
    }
    
    @IBInspectable
    public var date: Date = Date() {
        didSet {
            self.dateLabel.stringValue = dateFormatter.string(from: self.date);
        }
    }
    
    @IBInspectable
    public var dateFormat: String = "MMM d, h:mm a";
    
//    @IBInspectable
//    public var icon: NSImage = NSImage(named: NSImage.Name("More"))! {
//        didSet {
//            self.iconImageView.image = self.icon;
//        }
//    }
    
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
    
    private let dateFormatter: DateFormatter = DateFormatter();
    
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
        
//        backgroundIV.image = backgroundImage
//        backgroundIV.layer.cornerRadius = self.layer.cornerRadius
//        backgroundIV.clipsToBounds = true
//        backgroundIV.contentMode = .scaleAspectFill
//
//        backgroundIV.frame.origin = bounds.origin
//        backgroundIV.frame.size = CGSize(width: bounds.width, height: bounds.height)
//        contentInset = 6
    }
    
    @IBAction private func onOptionsButtonPressed(_ sender: NSButton) {
        
    }
    
}

extension OutlineCellView {
    private func setupView() {
        self.wantsLayer = true;
    }
}

extension OutlineCellView: CustomOperationSeparatable {
    func onLoadData() {
        
    }
    
    func onLayoutView() {
        
    }
}
