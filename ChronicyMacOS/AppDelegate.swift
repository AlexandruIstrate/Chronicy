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
    
    func applicationWillFinishLaunching(_ notification: Notification) {
        Log.target = ConsoleLogTarget();
        
        DistributedObjectManager.manager.keyStorage = UserDefaultsKeyStorage(suiteName: SharedConstants.appGroupSuiteName);
        DistributedObjectManager.manager.retrievalAction = .remove;
        
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
    
}

class MacOSTriggerRegister: TriggerManagerRegister {
    func triggers() -> [TriggerManager.Kind : ModuleTrigger] {
        return [.url : URLRecievedTrigger()];
    }
}
