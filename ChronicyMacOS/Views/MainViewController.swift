//
//  MainViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;

class MainViewController: NSViewController {
    
    public private(set) static var shared: MainViewController!;
    
    @IBOutlet private weak var sidebarView: SidebarView!;
    private var centerView: NSViewController?;
    
    override init(nibName nibNameOrNil: NSNib.Name?, bundle nibBundleOrNil: Bundle?) {
        super.init(nibName: nibNameOrNil, bundle: nibBundleOrNil);
        MainViewController.shared = self;
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder);
        MainViewController.shared = self;
    }
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.showCenterView(viewController: OutlineCentralViewController());
    }
    
    public func showCenterView(viewController: NSViewController) {
        if let oldViewController: NSViewController = self.centerView {
            oldViewController.view.removeFromSuperview();
        }
        
        self.centerView = viewController;
        
        let view: NSView = viewController.view;
        setupContentView(view: view);
    }
    
}

extension MainViewController {
    private func setupContentView(view: NSView) {
        self.view.addSubview(view);
        
        let cv: NSView = self.view;
        
        view.translatesAutoresizingMaskIntoConstraints = false;
        view.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        view.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        view.leadingAnchor.constraint(equalTo: sidebarView.trailingAnchor).isActive = true;
        view.trailingAnchor.constraint(equalTo: cv.trailingAnchor).isActive = true;
    }
}
