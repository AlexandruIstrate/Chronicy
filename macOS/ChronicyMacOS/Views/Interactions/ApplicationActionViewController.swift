//
//  ApplicationActionViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ApplicationActionViewController: ActionViewController<ApplicationAction> {
    @IBOutlet private weak var pathField: NSTextField!;
    @IBOutlet private weak var browseButton: NSButton!;
    
    private var path: String {
        get { return self.pathField.stringValue; }
        set { self.pathField.stringValue = newValue; }
    }
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
    }

    override func onChangeTo() {
        guard let action: ApplicationAction = self.action else {
            Log.error(message: "Action not set for ApplicationActionViewController!");
            return;
        }
        
        self.path = action.path;        
        super.onChangeTo();
    }
    
    override func onChangeAway() {
        self.action?.path = self.path;
        super.onChangeAway();
    }
    
    @IBAction private func onBrowseClicked(_ sender: NSButton) {
        self.displayFileDialog();
    }
    
    private func displayFileDialog() {
        let panel: NSOpenPanel = NSOpenPanel();
        panel.canChooseFiles = true;
        panel.canChooseDirectories = false;
        panel.allowsMultipleSelection = false;
        panel.allowedFileTypes = ["app"];
        
        guard panel.runModal() == .OK else {
            self.onCancel();
            return;
        }
        
        let url: URL = panel.urls[0];
        Log.info(message: url.absoluteString);
        self.onOK(url: url);
    }
    
    private func onOK(url: URL) {
        self.path = url.path;
        self.pathField.stringValue = url.path;
    }
    
    private func onCancel() {
        
    }
}
