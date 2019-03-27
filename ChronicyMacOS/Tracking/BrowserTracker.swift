//
//  BrowserTracker.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;
import ChronicyFramework;

typealias BrowserVersion = (major: Int, minor: Int, patch: Int);

enum Browser {
    case safari;
    case chrome;
    case firefox;
    case other(name: String);
}

class BrowserModule: Module {
    override func triggers() -> [ModuleTrigger] {
        return [ URLRecievedTrigger() ];
    }
    
    func browserName() -> Browser { fatalError("Method browserName is abstract and must be implemented in a subclass of this class."); }
    func browserVersion() -> BrowserVersion { fatalError("Method browserVersion is abstract and must be implemented in a subclass of this class."); }
    
    func launch(/* with: BrowserLaunchParams */) { fatalError("Method launch is abstract and must be implemented in a subclass of this class."); }
}

class URLRecievedTrigger: ModuleTrigger {
    static func key() -> String {
        return "URLRecieved";
    }
}
