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
    
    private var sidebarView: SidebarViewController!;
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
        
        self.setupSidebarView();
        self.setupModules();
        
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
    
    public func reloadContentViewData() {
        guard let view: ContentView = centerView as? ContentView else {
            return;
        }
        
        view.reloadData();
    }
    
}

extension MainViewController {    
    private func setupSidebarView() {
        self.sidebarView = SidebarViewController();
        self.view.addSubview(sidebarView.view);
        
        let cv: NSView = self.view;
        let sv: NSView = self.sidebarView.view;
        
        sv.translatesAutoresizingMaskIntoConstraints = false;
        sv.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        sv.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        sv.leadingAnchor.constraint(equalTo: cv.leadingAnchor).isActive = true;
    }
    
    private func setupContentView(view: NSView) {
        self.view.addSubview(view);
        
        let cv: NSView = self.view;
        let sv: NSView = self.sidebarView.view;
        
        view.translatesAutoresizingMaskIntoConstraints = false;
        view.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        view.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        view.leadingAnchor.constraint(equalTo: sv.trailingAnchor).isActive = true;
        view.trailingAnchor.constraint(equalTo: cv.trailingAnchor).isActive = true;
    }
    
    private func setupModules() {
//        ModuleManager.manager.add(module: SafariBrowserModule());
    }
}
