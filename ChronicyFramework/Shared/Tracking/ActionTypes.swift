//
//  ActionTypes.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Foundation;

public class CommandAction: Action {
    public var command: String = "";
    public var useSystemPrivileges: Bool = false;
    
    public override func onTrigger() {
        
    }
}

public class ApplicationAction: Action {
    public var path: String = "";
    
    public override func onTrigger() {
        do {
            try ApplicationManager.manager.launch(path: self.path);
        } catch {
            Log.error(message: "Could not launch application at path \(self.path)")
        }
    }
}
