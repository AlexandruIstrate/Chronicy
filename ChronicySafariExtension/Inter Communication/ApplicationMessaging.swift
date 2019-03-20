//
//  ApplicationMessaging.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 18/03/2019.
//

import Foundation;
import ChronicyFramework;

class ApplicationMessaging {
    
    public let shared: ApplicationMessaging = ApplicationMessaging();
    
    private init() {}
    
    public func sendURL(url: URL) {
        DistributedObjectManager.manager.set(object: url, for: "currentPageURL");
    }
}
