//
//  StackFieldEditorViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 01/05/2019.
//

import Cocoa;
import ChronicyFrameworkMacOS;

class StackFieldEditorViewController: NSViewController {

    @IBOutlet private weak var nameField: NSTextField!;
    @IBOutlet private weak var typePopUp: NSPopUpButton!;
    
    public var name: String = "";
    public var types: [String] = [];
    public var selectedType: String = "";
    
    typealias StackFieldEditorCallback = () -> ();
    public var callback: StackFieldEditorCallback?;
    
    override func viewDidLoad() {
        super.viewDidLoad();
        
        self.nameField.stringValue = self.name;
        self.typePopUp.addItems(withTitles: self.types);
        self.typePopUp.selectItem(withTitle: selectedType);
        
        if self.typePopUp.numberOfItems >= 0 {
            self.typePopUp.selectItem(withTitle: selectedType);
        }
    }
    
    override func viewDidDisappear() {
        super.viewDidDisappear();
        callback?();
    }
    
    @IBAction private func onNameChanged(_ sender: NSTextField) {
        self.name = self.nameField.stringValue;
    }
    
    @IBAction private func onTypeChanged(_ sender: NSPopUpButton) {
        guard let selectedItem: String = self.typePopUp.titleOfSelectedItem else {
            Log.error(message: "StackFieldEditor does not have a selected type!");
            return;
        }
        
        self.selectedType = selectedItem;
    }
}
