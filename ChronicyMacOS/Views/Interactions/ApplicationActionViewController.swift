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
    @IBOutlet private weak var browseButton: NSButton!;
    
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
        self.selectRadioButton.state = .on;
        
        self.setItemState();
    }
    
    override func onChangeAway() {
        self.action?.path = self.path;
    }
    
    @IBAction private func onInputTypeChanged(_ sender: NSButton) {
        self.setItemState();
    }
    
    @IBAction private func onBrowseClicked(_ sender: NSButton) {
        self.displayFileDialog();
    }
    
    private func setItemState() {
        choicePopUp.isEnabled = !self.usesExplicitPath;
        
        pathField.isEnabled = self.usesExplicitPath;
        browseButton.isEnabled = self.usesExplicitPath;
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
