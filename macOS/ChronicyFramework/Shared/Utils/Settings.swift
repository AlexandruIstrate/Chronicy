//
//  Settings.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 24/07/2019.
//

import Foundation;

public class Settings {
    
    public static var groupByTags: Bool {
        get { return UserDefaults.standard.bool(forKey: Keys.groupByTags); }
        set { UserDefaults.standard.set(newValue, forKey: Keys.groupByTags); }
    }
    
    public static var webServiceURL: String {
        get { return UserDefaults.standard.string(forKey: Keys.webServiceURL) ?? defaultServiceURL; }
        set { UserDefaults.standard.set(newValue, forKey: Keys.webServiceURL); }
    }
    
    // TODO: Change this to the real value
    public static let defaultServiceURL: String = "https://localhost";
    
    public static var username: String {
        get { return secureStorage.get(key: Keys.username) ?? ""; }
        set { secureStorage.set(value: newValue, key: Keys.username); }
    }
    
    public static var password: String {
        get { return secureStorage.get(key: Keys.password) ?? ""; }
        set { secureStorage.set(value: newValue, key: Keys.password); }
    }
    
    public class Keys {
        public static let groupByTags: String = "groupByTags";
        public static let webServiceURL: String = "webServiceURL";
        public static let username: String = "username";
        public static let password: String = "password";
    }
    
    public static func setupDefaults() {
        let fileName: String = "Defaults";
        let fileExtension: String = "plist";
        
        guard let fileUrl: URL = Bundle.main.url(forResource: fileName, withExtension: fileExtension) else {
            return;
        }
        
        let dictionary: NSDictionary = try! NSDictionary(contentsOf: fileUrl, error: ());
        UserDefaults.standard.register(defaults: dictionary as! [String : Any]);
    }
    
    public static var secureStorage: SecureStorage!;
}
