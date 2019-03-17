//
//  KeyStorage.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 17/03/2019.
//

import Foundation;

public protocol KeyStorage {
    func set<T>(object: T, for key: String) throws;
    func get<T>(for key: String) throws -> T;
}

public enum KeyStorageError: Error {
    case conversionError;
    case missingObject;
    case internalError;
}

public class UserDefaultsKeyStorage: KeyStorage {
    
    private var defaults: UserDefaults;
    
    init(suiteName: String) {
        self.defaults = UserDefaults(suiteName: suiteName)!;
    }
    
    init(defaults: UserDefaults) {
        self.defaults = defaults;
    }
    
    init() {
        self.defaults = UserDefaults.standard;
    }
    
    public func set<T>(object: T, for key: String) throws {
        defaults.set(object, forKey: key);
    }
    
    public func get<T>(for key: String) throws -> T {
        guard let object: Any = defaults.object(forKey: key) else {
            throw KeyStorageError.missingObject;
        }
        
        guard let typedObject: T = object as? T else {
            throw KeyStorageError.conversionError;
        }
        
        return typedObject;
    }
}
