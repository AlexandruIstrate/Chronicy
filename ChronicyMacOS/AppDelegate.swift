//
//  AppDelegate.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa;
import ChronicyFramework;

@NSApplicationMain
class AppDelegate: NSObject, NSApplicationDelegate {

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        // TODO: Move
        Log.target = ConsoleLogTarget();
        
        Log.info(message: "Application did finish launching");
    }

    func applicationWillTerminate(_ aNotification: Notification) {
        Log.info(message: "Application will terminate");
    }

    func applicationShouldTerminate(_ sender: NSApplication)-> NSApplication.TerminateReply {
        return .terminateNow;
    }

}
