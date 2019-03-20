//
//  KeyStorage.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 18/03/2019.
//

import Foundation;

public protocol KeyStorage {
    func set<T>(object: T, for key: String) throws;
    func get<T>(for key: String) throws -> T;
    func remove(for key: String);
}

public enum KeyStorageError: Error {
    case conversionError;
    case missingObject;
    case internalError;
}

public class MemoryKeyStorage: KeyStorage {
    
    private var storage: [String : Any] = [:];
    
    public func set<T>(object: T, for key: String) throws {
        storage[key] = object;
    }
    
    public func get<T>(for key: String) throws -> T {
        guard let value: Any = storage[key] else {
            throw KeyStorageError.missingObject;
        }
        
        guard let typedValue: T = value as? T else {
            throw KeyStorageError.conversionError;
        }
        
        return typedValue;
    }
    
    public func remove(for key: String) {
        storage.removeValue(forKey: key);
    }
}

public class UserDefaultsKeyStorage: KeyStorage {
    
    private var defaults: UserDefaults;
    
    public init(suiteName: String) {
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
    
    public func remove(for key: String) {
        defaults.removeObject(forKey: key);
    }
}
