//
//  SidebarViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 03/04/2019.
//

import Cocoa

class SidebarViewController: NSViewController {
    
    @IBOutlet private weak var itemView: NSCollectionView!;
    
    private var cells: [SidebarCell] = [];
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.setupItemView();
        
        self.cells.append(SidebarCell(title: nil, image: NSImage(named: NSImage.Name("Outline"))!, clickHandler: nil));
        self.itemView.reloadData();
    }
    
}

extension SidebarViewController: NSCollectionViewDataSource, NSCollectionViewDelegate {
    
    func collectionView(_ collectionView: NSCollectionView, numberOfItemsInSection section: Int) -> Int {
        return self.cells.count;
    }
    
    func collectionView(_ collectionView: NSCollectionView, itemForRepresentedObjectAt indexPath: IndexPath) -> NSCollectionViewItem {
        return SidebarViewItem();
    }
    
    func collectionView(_ collectionView: NSCollectionView, didSelectItemsAt indexPaths: Set<IndexPath>) {
        
    }
    
}

extension SidebarViewController {
    private func setupItemView() {
        self.itemView.register(NSNib(nibNamed: "SidebarViewItem", bundle: Bundle.main), forItemWithIdentifier: NSUserInterfaceItemIdentifier("SidebarCell"));
    }
}

class SidebarViewItem: NSCollectionViewItem {
    
}

struct SidebarCell {
    var title: String?;
    var image: NSImage?;
    
    typealias SidebarClickHandler = (SidebarCell) -> ();
    var clickHandler: SidebarClickHandler?;
}
