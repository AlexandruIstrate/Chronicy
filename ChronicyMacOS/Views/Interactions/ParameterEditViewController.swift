//
//  ParameterEditViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 14/05/2019.
//

import Cocoa

class ParameterEditViewController: NSViewController {

    @IBOutlet private weak var valueTextField: NSTextField!;
    
    typealias ParameterEditCallback = (String) -> ();
    public var callback: ParameterEditCallback?;
    
    public var value: String = "";
    
    override func viewDidLoad() {
        super.viewDidLoad();
        self.valueTextField.stringValue = self.value;
    }
    
    override func viewDidDisappear() {
        callback?(valueTextField.stringValue);
        super.viewDidDisappear();
    }
    
}
