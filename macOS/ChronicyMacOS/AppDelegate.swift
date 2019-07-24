//
//  AppDelegate.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

@NSApplicationMain
class AppDelegate: NSObject, NSApplicationDelegate {
    
    @IBOutlet private weak var localSourceMenu: NSMenuItem!;
    @IBOutlet private weak var webSourceMenu: NSMenuItem!;
    
    func applicationWillFinishLaunching(_ notification: Notification) {
        Log.target = ConsoleLogTarget();
        configureSettings();
        
        DistributedObjectManager.manager.keyStorage = UserDefaultsKeyStorage(suiteName: SharedConstants.appGroupSuiteName);
        DistributedObjectManager.manager.retrievalAction = .remove;
        
        ApplicationManager.manager.launcher = MacOSApplicationLauncher();
        
        ModuleManager.manager.add(module: SafariBrowserModule());
        TriggerManager.manager.register(register: MacOSTriggerRegister());
    }

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        Log.info(message: "Application did finish launching");
        
        localSourceMenu.state = .on;
        webSourceMenu.state = .off;
    }

    func applicationWillTerminate(_ aNotification: Notification) {
        Log.info(message: "Application will terminate");
    }

    func applicationShouldTerminate(_ sender: NSApplication)-> NSApplication.TerminateReply {
        return .terminateNow;
    }
    
    private func configureSettings() {
        Settings.setupDefaults();
        Settings.secureStorage = KeychainStorage();
    }
    
    @IBAction private func onPreferencesClicked(_ sender: NSMenuItem) {
        let storyboardName: String = "Main";
        let windowName: String = "PreferencesWindow";
        let controllerName: String = "PreferencesController";
        
        let storyboard: NSStoryboard = NSStoryboard(name: NSStoryboard.Name(storyboardName), bundle: nil);
        let windowController: NSWindowController = storyboard.instantiateController(withIdentifier: NSStoryboard.SceneIdentifier(windowName)) as! PreferencesWindowController;
        let viewController: NSViewController = storyboard.instantiateController(withIdentifier: controllerName) as! NSViewController;
        
        let window: NSWindow? = windowController.window;
        window?.contentViewController = viewController;
        window?.makeKeyAndOrderFront(nil);
    }
    
    @IBAction private func onLocalSourceSelected(_ sender: NSMenuItem) {
        DataSourceManager.manager.sourceType = .local;
        
        localSourceMenu.state = .on;
        webSourceMenu.state = .off;
    }
    
    @IBAction private func onWebSourceSelected(_ sender: NSMenuItem) {
        DataSourceManager.manager.sourceType = .web;
        
        localSourceMenu.state = .off;
        webSourceMenu.state = .on;
    }
}

class MacOSTriggerRegister: TriggerManagerRegister {
    func triggers() -> [TriggerManager.Kind : ModuleTrigger] {
        return [.url : URLRecievedTrigger()];
    }
}
