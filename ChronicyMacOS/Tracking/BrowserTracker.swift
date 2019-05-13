//
//  BrowserTracker.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 10/03/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;

typealias BrowserVersion = (major: Int, minor: Int, patch: Int);

enum Browser {
    case safari;
    case chrome;
    case firefox;
    case other(name: String);
}

class BrowserModule: Module {
    func browserName() -> Browser { fatalError("Method browserName is abstract and must be implemented in a subclass of this class."); }
    func browserVersion() -> BrowserVersion { fatalError("Method browserVersion is abstract and must be implemented in a subclass of this class."); }
    
    func launch(/* with: BrowserLaunchParams */) { fatalError("Method launch is abstract and must be implemented in a subclass of this class."); }
}

public class URLRecievedTrigger: ModuleTrigger {
    public required init() {
        super.init(triggerName: "URLRecievedTrigger", moduleName: "BrowserModule");
    }
}
