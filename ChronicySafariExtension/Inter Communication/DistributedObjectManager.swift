//
//  DistributedObjectManager.swift
//  ChronicySafariExtension
//
//  Created by Alexandru Istrate on 18/03/2019.
//

import Foundation;

public struct DistributedObject<T: Codable>: Codable {
    var object: T;
    var dispatchDate: Date;
    var retrieved: Bool;
    
    public init(object: T) {
        self.object = object;
        self.dispatchDate = Date();
        self.retrieved = false;
    }
}

public class DistributedObjectManager {
    
    public static var manager: DistributedObjectManager = DistributedObjectManager();
    
    private var subscribers: [ObjectManagerDelegate] = [];
    
    public var keyStorage: KeyStorage = MemoryKeyStorage();
    public var encoding: String.Encoding = .utf8;
    
    public enum OnRetrievalAction {
        case keepUnchanged, invalidate, remove;
    }
    
    public var retrievalAction: OnRetrievalAction = .invalidate;
    
    private init() {}
    
    public func get<T: Codable>(for key: String) -> T? {
        do {
            let decoder: JSONDecoder = JSONDecoder();
            
            let objectString: String = try keyStorage.get(for: key);
            
            guard let data: Data = objectString.data(using: encoding) else {
                return nil;
            }
            
            var decodedObject: DistributedObject<T> = try decoder.decode(DistributedObject<T>.self, from: data);
            
            guard !decodedObject.retrieved else {
                return nil;
            }
            
            switch retrievalAction {
            case .keepUnchanged:
                break;
                
            case .invalidate:
                decodedObject.retrieved = true;
                self.set(object: decodedObject, for: key);
                
            case .remove:
                keyStorage.remove(for: key);
            }
            
            return decodedObject.object;
        } catch let e as KeyStorageError {
//            Log.error(message: "An error occurred while retrieving object for key \(key): \(e.localizedDescription)");
            return nil;
        } catch {
//            Log.error(message: "An unknown occurred while retrieving object for key \(key).");
            return nil;
        }
    }
    
    public func set<T: Codable>(object: T, for key: String) {
        do {
            let encoder: JSONEncoder = JSONEncoder();
            let data: Data = try encoder.encode(DistributedObject<T>(object: object));
            
            guard let string: String = String(data: data, encoding: encoding) else {
//                Log.error(message: "Could not convert object data to String with encoding \(encoding.description).");
                return;
            }
            
            try keyStorage.set(object: string, for: key);
        } catch let e as KeyStorageError {
//            Log.error(message: "An error occurred while setting object for key \(key): \(e.localizedDescription)");
        } catch {
//            Log.error(message: "An unknown occurred while setting object for key \(key).");
        }
    }
    
    public func subscribe(subscriber: ObjectManagerDelegate) {
        self.subscribers.append(subscriber);
    }
}

public protocol ObjectManagerDelegate {
    func onModified();
}
