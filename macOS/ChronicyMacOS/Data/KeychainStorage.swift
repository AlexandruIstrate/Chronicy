//
//  KeychainStorage.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;
import KeychainSwift;

public class KeychainStorage: SecureStorage {
    
    private let keychain: KeychainSwift = KeychainSwift();
    
    public func set(value: String, key: String) {
        keychain.set(value, forKey: key);
    }
    
    public func set(value: Bool, key: String) {
        keychain.set(value, forKey: key);
    }
    
    public func set(value: Data, key: String) {
        keychain.set(value, forKey: key);
    }
    
    public func get(key: String) -> String? {
        return keychain.get(key);
    }
    
    public func get(key: String) -> Bool? {
        return keychain.getBool(key);
    }
    
    public func get(key: String) -> Data? {
        return keychain.getData(key);
    }
    
    public func delete(key: String) {
        keychain.delete(key);
    }
    
}
