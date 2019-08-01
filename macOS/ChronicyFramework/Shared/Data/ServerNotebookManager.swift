//
//  ServerNotebookManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/04/2019.
//

import Foundation;

public class ServerNotebookManager: NotebookManager {
    
    private var api: WebAPI = WebAPI.shared;
    
    public func setup(callback: @escaping NotebookManagerSetupCallback) {
        api.authenticate(username: Settings.username, password: Settings.password) { (error: APIError?, token: Token?) in
            guard error == nil else {
                callback(.authenticationFailure);
                return;
            }
            
            guard let token: Token = token else {
                callback(.authenticationFailure);
                return;
            }
            
            guard !token.accessToken.isEmpty else {
                callback(.authenticationFailure);
                return;
            }
            
            callback(nil);
        };
    }
    
    public func getInfo(callback: @escaping NotebookManagerInfoCallback) {
        self.api.getNotebooks { (notebooks: [Notebook]?, error: APIError?) in
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
        self.api.getNotebook(id: Int(info.id)!) { (notebook: Notebook?, error: APIError?) in
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
    
    public func retrieveAllNotebooks(callback: @escaping NotebookManagerNotebooksCallback) {
        self.api.getNotebooks { (notebooks: [Notebook]?, error: APIError?) in
            guard error == nil else {
                callback(nil, .fetchFailure);
                return;
            }
            
            guard let notebooks: [Notebook] = notebooks else {
                callback(nil, .itemNotFound);
                return;
            }
            
            callback(notebooks, nil);
        }
    }
    
    public func saveNotebook(notebook: Notebook, callback: @escaping NotebookManagerSaveCallback) {
        self.api.updateNotebook(notebook: notebook, id: notebook.id) { (error: APIError?) in
            guard error == nil else {
                Log.error(message: "Could not save notebook with name \(notebook.name)");
                callback(NotebookManagerError.saveFailure);
                return;
            }
            
            callback(nil);
        }
    }
}
