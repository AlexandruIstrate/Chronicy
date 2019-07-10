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
        
        DistributedObjectManager.manager.keyStorage = UserDefaultsKeyStorage(suiteName: SharedConstants.appGroupSuiteName);
        DistributedObjectManager.manager.retrievalAction = .remove;
        
        ApplicationManager.manager.launcher = MacOSApplicationLauncher();
        
        ModuleManager.manager.add(module: SafariBrowserModule());
        TriggerManager.manager.register(register: MacOSTriggerRegister());
    }

    func applicationDidFinishLaunching(_ aNotification: Notification) {
//        Log.info(message: "Application did finish launching");
    }

    func applicationWillTerminate(_ aNotification: Notification) {
//        Log.info(message: "Application will terminate");
    }

    func applicationShouldTerminate(_ sender: NSApplication)-> NSApplication.TerminateReply {
        return .terminateNow;
    }
    
    private func registerTriggers() {
//        TriggerManager.manager
    }
    
    @IBAction private func onLocalSourceSelected(_ sender: NSMenuItem) {
        DataSourceManager.manager.sourceType = .local;
        webSourceMenu.state = .off;
    }
    
    @IBAction private func onWebSourceSelected(_ sender: NSMenuItem) {
        DataSourceManager.manager.sourceType = .web;
        localSourceMenu.state = .off;
    }
}

class MacOSTriggerRegister: TriggerManagerRegister {
    func triggers() -> [TriggerManager.Kind : ModuleTrigger] {
        return [.url : URLRecievedTrigger()];
    }
}
