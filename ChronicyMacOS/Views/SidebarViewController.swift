//
//  SidebarViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 03/04/2019.
//

import Cocoa

class SidebarViewController: NSViewController {

    @IBOutlet private weak var navigationButtonsView: NSStackView!;
    
    private var cells: [SidebarCell] = [];
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.cells.append(SidebarCell(title: nil, image: NSImage(named: NSImage.Name("Outline"))!, clickHandler: nil));
        
        self.loadData();
    }
    
}

extension SidebarViewController {
    private func loadData() {
        for cell: SidebarCell in cells {
            self.navigationButtonsView.addView(buttonForCell(cell: cell), in: .center);
        }
    }
    
    private func buttonForCell(cell: SidebarCell) -> NSButton {
        let button: NSButton = NSButton();

        
        if let title: String = cell.title {
            button.title = title;
        }
        
        if let image: NSImage = cell.image {
            button.image = image;
        }
        
        return button;
    }
}

struct SidebarCell {
    var title: String?;
    var image: NSImage?;
    
    typealias SidebarClickHandler = (SidebarCell) -> ();
    var clickHandler: SidebarClickHandler?;
}
