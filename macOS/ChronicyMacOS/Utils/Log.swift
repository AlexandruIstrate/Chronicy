//
//  Log.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;
import ChronicyFrameworkMacOS;

class ConsoleLogTarget: LogTarget {
    func log(message: String) {
        NSLog(message);
    }
}

class FileLogTarget: LogTarget {
    func log(message: String) {
        
    }
}
