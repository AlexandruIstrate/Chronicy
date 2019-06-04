//
//  NotificationSender.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 27/03/2019.
//

import Cocoa;

class NotificationSender: NSObject {
    
    public static let `default`: NotificationSender = NotificationSender();
    
    private var center: NSUserNotificationCenter = NSUserNotificationCenter.default;
    
    private override init() {
        super.init();
        self.center.delegate = self;
    }
    
    public func show(title: String, subtitle: String? = nil, infoText: String? = nil, image: NSImage? = nil) {
        let notification: NSUserNotification = NSUserNotification();
        notification.title = title;
        notification.subtitle = subtitle;
        notification.contentImage = image;
//        notification.soundName = NSUserNotificationDefaultSoundName;
        
        center.deliver(notification);
    }
}

extension NotificationSender: NSUserNotificationCenterDelegate {
    func userNotificationCenter(_ center: NSUserNotificationCenter, shouldPresent notification: NSUserNotification) -> Bool {
        return true;
    }
}
