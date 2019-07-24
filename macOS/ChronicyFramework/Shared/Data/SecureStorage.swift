//
//  SecureStorage.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;

public protocol SecureStorage {
    func set(value: String, key: String);
    func set(value: Bool, key: String);
    func set(value: Data, key: String);
    
    func get(key: String) -> String?;
    func get(key: String) -> Bool?;
    func get(key: String) -> Data?;
    
    func delete(key: String);
}
