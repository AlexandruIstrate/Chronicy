//
//  ActionViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ActionViewController<T>: NSViewController where T : Action {
    public var action: T?;
    
    override func viewDidAppear() {
        super.viewDidAppear();
        self.onChangeTo();
    }
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
        self.onChangeAway();
    }
    
    public func onChangeTo() {
        
    }
    
    public func onChangeAway() {
        
    }
    
    public func onTrigger() {
        self.action?.onTrigger();
    }
}

extension Action {
    var viewController: NSViewController {
        switch self {
        case is CommandAction:
            let vc: CommandActionViewController = CommandActionViewController();
            vc.action = self as? CommandAction;
            return vc;
        case is ApplicationAction:
            let vc: ApplicationActionViewController = ApplicationActionViewController();
            vc.action = self as? ApplicationAction;
            return vc;
        default:
            return NSViewController();
        }
    }
}
