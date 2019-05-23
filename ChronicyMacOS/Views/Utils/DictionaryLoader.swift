//
//  DictionaryLoader.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 16/05/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;

class BundlePlistDictionaryLoader: DictionaryLoader {
    public var fileName: String;
    
    public init(fileName: String) {
        self.fileName = fileName;
    }
    
    func dictionary() -> [String : String] {
        guard let path: String = Bundle.main.path(forResource: self.fileName, ofType: "plist") else {
            Log.error(message: "Could not find \(self.fileName).plist in bundle!");
            return [:];
        }
        
        guard let dict: [String : String] = NSDictionary(contentsOfFile: path) as? [String : String] else {
            Log.error(message: "Could not load keys in \(self.fileName).plist!");
            return [:];
        }
        
        return dict;
    }
}
