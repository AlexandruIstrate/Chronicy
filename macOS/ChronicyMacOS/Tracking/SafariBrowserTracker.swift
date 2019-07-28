//
//  SafariBrowserTracker.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 17/03/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;

class SafariBrowserModule: BrowserModule {
    
    public var currentPage: CurrentPageProperty = CurrentPageProperty();
    
    override func browserName() -> Browser {
        return .safari;
    }
    
    override func browserVersion() -> BrowserVersion {
        return (0, 0, 0);
    }
    
    override func launch() {
        Log.info(message: "Launching Safari...");
        
        do {
            try ApplicationManager.manager.launch(defaultApplication: .safari);
        } catch let e {
            Log.error(message: "Could not launch Safari! Reason: \(e.localizedDescription)");
            return;
        }
        
        Log.info(message: "Succesfuly launched Safari!");
    }
    
    override func onLoad() {
        setupProperties();
    }
    
    override func moduleName() -> String {
        return "SafariBrowserModule";
    }
    
    override func priority() -> ModulePriority {
        return .low;    // We don't want to slow down browsing
    }    
}

extension SafariBrowserModule {
    class CurrentPageProperty: TrackableProperty {
        
        public var url: URL?;
        
        func onRefreshData() {
            self.url = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.pageURLData, action: .remove);
            Log.info(message: "URL is \(String(describing: url))");
            
            guard let notebookName: String = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.browserSelectedNotebook, action: .keepUnchanged) else {
                Log.error(message: "Could not get notebook name!");
                return;
            }
            
            guard let stackName: String = DistributedObjectManager.manager.get(for: SharedConstants.DistributedObjectKeys.browserSelectedStack, action: .keepUnchanged) else {
                Log.error(message: "Could not get stack name!");
                return;
            }
            
            let notebookManager: NotebookManager = NotebookManagerFactory.create(type: DataSourceManager.manager.sourceType)
            notebookManager.named(name: notebookName) { (notebook: Notebook?, error: NotebookManagerError?) in
                guard let notebook: Notebook = notebook else {
                    Log.error(message: "Could not find notebook named \(notebookName)!");
                    return;
                }
                
                guard let stack: Stack = notebook.findStack(named: stackName) else {
                    Log.error(message: "Could not find stack named \(stackName)!");
                    return;
                }
                
                guard let url: URL = self.url else {
                    Log.error(message: "URL is nil!");
                    return;
                }
                
                let urlString: String = url.absoluteString;
                Log.info(message: "URL is \(urlString)");
                
                // TODO: Make a template system
                var card: Card = stack.insertNewCard();
                card.name = NSLocalizedString("Visited Webpage", comment: "");
                
                do {
                    try card.insertIntoFields(values: [urlString]);
                    try notebookManager.saveNotebook(notebook: notebook);
                    
                    ActivityManager.manager.add(withTitle: "New page visited from Safari", comment: "\(urlString)");
                    
                    self.notifyMainView();
                    TriggerManager.manager.raise(kind: .url);
                } catch let e as InsertionError {
                    Log.error(message: "Could not insert into card: \(e)");
                } catch let e {
                    Log.error(message: "Could not save notebook named \(notebook.name): \(e)");
                }
            }
        }
        
        private func notifyMainView() {
            guard let vc: OutlineCentralViewController = MainViewController.shared.centerViewAs() else {
                return;
            }
            
            vc.notifyFieldsInserted();
        }
    }
}

extension SafariBrowserModule {
    private func setupProperties() {
        self.properties = [ currentPage ];
    }
}
