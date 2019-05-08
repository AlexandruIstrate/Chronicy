//
//  MainViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;
import FatSidebar;

class MainViewController: NSViewController {
    
    public private(set) static var shared: MainViewController!;
    
    @IBOutlet private weak var sidebar: FatSidebar!
    private var centerView: NSViewController?;
    
    private var pageManager: PageManager = PageManager();
    
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
        
        self.setupPages();
        self.addSidebarItems();
        
        self.setupSidebarView();
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

extension MainViewController: FatSidebarDelegate {
    func sidebar(_ sidebar: FatSidebar, didChangeSelection selectionIndex: Int) {
        self.pageManager.select(at: selectionIndex);
    }
}

extension MainViewController {
    private func setupSidebarView() {
        self.sidebar.theme = SidebarTheme();
        self.sidebar.style = .small(iconSize: 30, padding: 8);
        self.sidebar.selectionMode = .toggleOne;
        self.sidebar.delegate = self;
    }
    
    private func setupPages() {
        self.pageManager.add(page: Page(title: NSLocalizedString("Outline", comment: ""), icon: NSImage(named: NSImage.Name("Outline"))!, tint: NSColor(named: NSColor.Name("")), onLoad: { (page: Page) in
            self.showCenterView(viewController: OutlineCentralViewController());
        }, onUnload: { (page: Page) in

        }));
        self.pageManager.add(page: Page(title: NSLocalizedString("Interactions", comment: ""), icon: NSImage(named: NSImage.Name("Interaction"))!, tint: NSColor(named: NSColor.Name("")), onLoad: { (page: Page) in
            self.showCenterView(viewController: InteractionsViewController());
        }, onUnload: { (page: Page) in
            
        }));
        self.pageManager.add(page: Page(title: NSLocalizedString("Activity", comment: ""), icon: NSImage(named: NSImage.Name("Activity"))!, tint: NSColor(named: NSColor.Name("")), onLoad: { (page: Page) in
            self.showCenterView(viewController: ActivityViewController());
        }, onUnload: { (page: Page) in
            
        }));
    }
    
    private func setupContentView(view: NSView) {
        self.view.addSubview(view);
        
        let cv: NSView = self.view;
        let sv: NSView = self.sidebar;
        
        view.translatesAutoresizingMaskIntoConstraints = false;
        view.topAnchor.constraint(equalTo: cv.topAnchor).isActive = true;
        view.bottomAnchor.constraint(equalTo: cv.bottomAnchor).isActive = true;
        view.leadingAnchor.constraint(equalTo: sv.trailingAnchor).isActive = true;
        view.trailingAnchor.constraint(equalTo: cv.trailingAnchor).isActive = true;
    }
    
    private func addSidebarItems() {
        self.resetSidebar();
        
        for page: Page in self.pageManager.pages {
            sidebar.appendItem(page);
        }
        
        self.addSidebarTags();
        
        if self.sidebar.itemCount > 0 {
            self.sidebar.selectItem(at: 0);
        }
    }
    
    private func addSidebarTags() {
//        for tag: CardTag in CardTagManager.manager.tags {
//            sidebar.appendItem(Page(title: tag.title, icon: NSImage(named: NSImage.Name("Tag"))!, tint: tag.nsColor, onLoad: { (page: Page) in
//                Log.info(message: page.title);
//            }));
//        }
    }
    
    private func resetSidebar() {
        self.sidebar.removeAllItems();
    }
}
