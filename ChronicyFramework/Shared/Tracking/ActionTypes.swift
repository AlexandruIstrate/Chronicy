//
//  ActionTypes.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Cocoa;

public class CommandAction: Action {
    public var command: String = "";
    public var params: [String] = [];
    public var useSystemPrivileges: Bool = false;
    
    public var fullCommand: String {
        return "\(command) \(params.joined(separator: " "))";
    }
    
    public override func onTrigger() {
        let task: Process = Process();
        task.launchPath = "/bin/sh";
        
        print(self.fullCommand);
        
        let arguments: [String] = ["-c", self.fullCommand];
        task.arguments = arguments;
        
        let pipe: Pipe = Pipe();
        task.standardOutput = pipe;
        
        let handle: FileHandle = pipe.fileHandleForReading;
        
        task.launch();
        
        let data: Data = handle.readDataToEndOfFile();
        Log.info(message: "Command output: \(String(data: data, encoding: .utf8) ?? "Nothing")");
        
        super.onTrigger();
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
    
    public static func defaultApplications(dictionaryLoader: DictionaryLoader) -> [String : String] {
        return [
            "Calculator" : "/Applications/Calculator.app",
            "Mail" : "/Applications/Mail.app",
            "Notes" : "/Applications/Notes.app"
        ];
        
//        return dictionaryLoader.dictionary();
    }
    
    public override func onTrigger() {
        do {
            try ApplicationManager.manager.launch(path: self.path);
        } catch {
            Log.error(message: "Could not launch application at path \(self.path)")
        }
        
        super.onTrigger();
    }
}
