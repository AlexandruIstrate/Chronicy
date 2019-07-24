//
//  PreferencesServicesViewController.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;
import Cocoa;
import ChronicyFrameworkMacOS;

public class PreferencesServicesViewController: NSViewController {
    
    @IBOutlet private weak var usernameTextField: NSTextField!;
    @IBOutlet private weak var passwordTextField: NSSecureTextField!;
    
    private var usernameFieldDelegate: CredentialsTextFieldDelegate?;
    private var passwordFieldDelegate: CredentialsTextFieldDelegate?;
    
    public override func viewDidLoad() {
        super.viewDidLoad();
        setupCredentialFields();
        
        usernameTextField.stringValue = Settings.username;
        passwordTextField.stringValue = Settings.password;
    }
    
    @IBAction private func onUseDefaultClicked(_ sender: NSButton) {
        
    }
    
    private func setupCredentialFields() {
        usernameFieldDelegate = CredentialsTextFieldDelegate(field: usernameTextField, onEditFinished: { (text: String) in
            Settings.username = text;
        });
        
        passwordFieldDelegate = CredentialsTextFieldDelegate(field: passwordTextField, onEditFinished: { (text: String) in
            Settings.password = text;
        });
        
        usernameTextField.delegate = usernameFieldDelegate;
        passwordTextField.delegate = passwordFieldDelegate;
    }
}

fileprivate class CredentialsTextFieldDelegate: NSObject, NSTextFieldDelegate {
    
    typealias EditFinishedDelegate = (String) -> ();
    var onEditFinished: EditFinishedDelegate?;
    
    private weak var textField: NSTextField?;
    
    init(field: NSTextField, onEditFinished: EditFinishedDelegate? = nil) {
        self.textField = field;
        self.onEditFinished = onEditFinished;
    }
    
    func controlTextDidEndEditing(_ obj: Notification) {
        onEditFinished?(textField!.stringValue);
    }
}
