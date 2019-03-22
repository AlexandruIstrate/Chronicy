//
//  OutlineViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class OutlineViewController: NSViewController {

    @IBOutlet private weak var stackView: NSStackView!;
    
    private var internalStackCount: Int = 0;
    private var stacks: [OutlineStackView] = [];
    
    public var dataSource: OutlineViewDataSource?;
    
    override init(nibName nibNameOrNil: NSNib.Name?, bundle nibBundleOrNil: Bundle?) {
        super.init(nibName: nibNameOrNil, bundle: nibBundleOrNil);
        setupObservers();
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        setupObservers();
    }
    
    public func reloadData() {
        onLoadData();
        onLayoutView();
    }

}

extension OutlineViewController: CustomOperationSeparatable {
    func onLoadData() {
        guard let dataSource: OutlineViewDataSource = self.dataSource else {
            return;
        }
        
        self.internalStackCount = dataSource.stackCount();
        
        for i in 0..<self.internalStackCount {
            let stack: OutlineStackView = dataSource.stack(for: self, at: i);
            self.stacks.append(stack);
            
            stack.onLoadData();
        }
    }
    
    func onLayoutView() {
        for i in 0..<self.internalStackCount {
            let stack: OutlineStackView = stacks[i];
            self.stackView.addView(stack, in: .leading);
            
            stack.onLayoutView();
        }

    }
}

extension OutlineViewController {
    private func setupObservers() {
        NotificationCenter.default.addObserver(self, selector: #selector(onAdd), name: WindowController.Notifications.toolbarAdd.name, object: nil);
        NotificationCenter.default.addObserver(self, selector: #selector(onRemove), name: WindowController.Notifications.toolbarRemove.name, object: nil);
        NotificationCenter.default.addObserver(self, selector: #selector(onEdit), name: WindowController.Notifications.toolbarEdit.name, object: nil);
    }
    
    @objc
    private func onAdd(notification: Notification) {
        Log.info(message: "onAdd");
    }
    
    @objc
    private func onRemove(notification: Notification) {
        Log.info(message: "onRemove");
    }
    
    @objc
    private func onEdit(notification: Notification) {
        Log.info(message: "onEdit");
    }

}

protocol OutlineViewDataSource {
    func stackCount() -> Int;
    func stack(for view: OutlineViewController, at index: Int) -> OutlineStackView;
}
