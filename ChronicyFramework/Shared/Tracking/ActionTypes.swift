//
//  ActionTypes.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Foundation;

public class CommandAction: Action {
    public var command: String = "";
    public var params: [String] = [];
    public var useSystemPrivileges: Bool = false;
    
    public override func onTrigger() {
        
    }
    
    @discardableResult
    public func insertNewParameter() -> String {
        let nameRoot: String = "param";
        var name: String = nameRoot;
        var index: Int = 1;
        
        while params.contains(where: { (iter: String) -> Bool in
            return iter == name;
        }) {
            name = nameRoot + String(index);
            index += 1;
        }
        
        params.append(name);
        return name;
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
