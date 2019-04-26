//
//  SharedConstants.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/03/2019.
//

import Foundation;

public class SharedConstants {
    public static var appGroupSuiteName: String = "extension.ro.internals";
    
    public class DistributedObjectKeys {
        public static var pageURL: String = NSLocalizedString("currentPageURL", comment: "");
        
        public static var notebooks: String = "notebooks";
        public static var stacks: String = "tasks";
        
        public static var browserSelectedStack: String = "browserSelectedTask";
    }
}
