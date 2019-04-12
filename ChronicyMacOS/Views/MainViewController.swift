//
//  MainViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;
import ChronicyFramework;
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
        self.sidebar.style = .small(iconSize: 24, padding: 6);
        self.sidebar.selectionMode = .toggleOne;
        self.sidebar.delegate = self;
    }
    
    private func setupPages() {
        let image: NSImage = NSImage(named: NSImage.Name("Outline"))!;
        let color: NSColor? = NSColor(named: NSColor.Name(""));
        
        let page: Page = Page(title: NSLocalizedString("Outline", comment: ""), icon: image, tint: color, onLoad: { (page: Page) in

        }, onUnload: { (page: Page) in

        });
        
        self.pageManager.add(page: page);
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
        for tag: CardTag in CardTagManager.manager.tags {
            sidebar.appendItem(Page(title: tag.title, icon: NSImage(named: NSImage.Name("Tag"))!, tint: tag.nsColor, onLoad: { (page: Page) in
                Log.info(message: page.title);
            }));
        }
    }
    
    private func resetSidebar() {
        self.sidebar.removeAllItems();
    }
}
