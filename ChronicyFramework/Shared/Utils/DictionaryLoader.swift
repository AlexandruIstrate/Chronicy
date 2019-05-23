//
//  DictionaryLoader.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 16/05/2019.
//

import Foundation;

public protocol DictionaryLoader {
    func dictionary() -> [String : String];
}
