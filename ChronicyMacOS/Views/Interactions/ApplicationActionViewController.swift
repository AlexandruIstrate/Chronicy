//
//  ApplicationActionViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 11/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class ApplicationActionViewController: ActionViewController<ApplicationAction> {
    @IBOutlet private weak var choicePopUp: NSPopUpButton!;
    @IBOutlet private weak var pathField: NSTextField!;
    
    @IBOutlet private weak var selectRadioButton: NSButton!;
    @IBOutlet private weak var typeInPathRadioButton: NSButton!;
    
    private var usesExplicitPath: Bool {
        get { return self.typeInPathRadioButton.state == .on; }
        set {
            if newValue {
                self.typeInPathRadioButton.state = .on;
            } else {
                self.selectRadioButton.state = .on;
            }
        }
    }
    
    private var path: String {
        get {
            if usesExplicitPath {
                return self.typeInPathRadioButton.stringValue;
            }
            
            return self.selectRadioButton.stringValue;
        }
        set {
            if usesExplicitPath {
                self.typeInPathRadioButton.stringValue = newValue;
            } else {
                self.selectRadioButton.stringValue = newValue;
            }
        }
    }

    override func onChangeTo() {
        guard let action: ApplicationAction = self.action else {
            Log.error(message: "Action not set for ApplicationActionViewController!");
            return;
        }
        
        self.path = action.path;
    }
    
    override func onChangeAway() {
        self.action?.path = self.path;
    }
    
    @IBAction private func onInputTypeChanged(_ sender: NSButton) {
        
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
        self.path = url.absoluteString;
    }
    
    private func onCancel() {
        
    }
}
