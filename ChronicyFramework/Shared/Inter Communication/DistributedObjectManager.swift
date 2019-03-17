//
//  DistributedObjectManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 17/03/2019.
//

import Foundation;

public class DistributedObjectManager {
    
    private var subscribers: [ObjectManagerDelegate] = [];
    public var keyStorage: KeyStorage!;
    
    public func get<T: Codable>(for key: String) -> T? {
        do {
            let decoder: JSONDecoder = JSONDecoder();
            
            let object: String = try keyStorage.get(for: key);
            
            guard let decodedObject: T = decoder.decode(T.self, from: object.data(using: .utf8)) else {
                return nil;
            }
            
            return decodedObject;
        } catch let e as KeyStorageError {
            Log.error(message: "An error occurred while retrieving object for key \(key): \(e.localizedDescription)");
            return nil;
        } catch {
            Log.error(message: "An unknown occurred while retrieving object for key \(key).");
            return nil;
        }
    }
    
    public func set<T: Codable>(object: T, for key: String) {
        do {
            try keyStorage.set(object: object, for: key);
        } catch let e as KeyStorageError {
            Log.error(message: "An error occurred while setting object for key \(key): \(e.localizedDescription)");
        } catch {
            Log.error(message: "An unknown occurred while setting object for key \(key).");
        }
    }
    
    public func subscribe(subscriber: ObjectManagerDelegate) {
        self.subscribers.append(subscriber);
    }
}

// TODO: Change to something that doesn't just work with UserDefaults
extension DistributedObjectManager {
    private func setupUpdates() {
        NotificationCenter.default.addObserver(self, selector: #selector(onNotificaiton), name: UserDefaults.didChangeNotification, object: nil);
    }
    
    @objc
    private func onNotificaiton() {
        Log.info(message: "New notification recieved!");
    }
}

public protocol ObjectManagerDelegate {
    func onModified();
}
