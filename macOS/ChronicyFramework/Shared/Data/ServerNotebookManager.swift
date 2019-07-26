//
//  ServerNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class ServerNotebookManager: NotebookManager {
    
    private var api: WebAPI = WebAPI.shared;
    
    // TODO: Change
    public init() {
        authenticate();
    }
    
    public func authenticate() {
        api.authenticate(username: Settings.username, password: Settings.password) { (error: Error?, token: Token?) in
            guard error == nil else {
                print(error ?? "Could not authenticate");   // TODO: Handle
                return;
            }
            
            guard let token: Token = token else {
                Log.error(message: "Could not authenticate");
                return;
            }
        };
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoCallback) {
        self.api.getNotebooks { (notebooks: [Notebook]?, error: Error?) in
            guard error == nil else {
                callback(nil, .fetchFailure);
                return;
            }
            
            guard let notebooks: [Notebook] = notebooks else {
                callback(nil, .itemNotFound);
                return;
            }
            
            let info: [NotebookInfo] = notebooks.map({ (iter: Notebook) -> NotebookInfo in
                return NotebookInfo(name: iter.name, id: String(iter.id), dateCreated: Date());
            })
            callback(info, nil);
        }
    }
    
    public func retrieveNotebook(info: NotebookInfo, callback: @escaping NotebookManagerNotebookCallback) {
        self.api.getNotebook(id: Int(info.id)!) { (notebook: Notebook?, error: Error?) in
            guard error == nil else {
                callback(nil, .fetchFailure);
                return;
            }
            
            guard let notebook: Notebook = notebook else {
                callback(nil, .itemNotFound);
                return;
            }
            
            callback(notebook, nil);
        }
    }
    
    public func saveNotebook(notebook: Notebook) throws {
        self.api.updateNotebook(notebook: notebook, id: /* notebook.id */ 0) { (error: Error?) in
            guard error == nil else {
                Log.error(message: "Could not save notebook with name \(notebook.name)");
                return;
            }
        }
    }
}
