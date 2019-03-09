//
//  Log.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;

public class Log {
    public enum Level: String {
        case info;      // Plain old info
        case warning;   // Something that shouldn't really happen, happened
        case error;     // Something bad happened
        case fatal;     // An irrecoverable error occurred
    }
    
    private static let dispatchQueue: DispatchQueue = DispatchQueue(label: "LogDispatchQueue");
    private static var logTarget: LogTarget = EmptyLogTarget();
    
    public static var target: LogTarget {
        get {
            var result: LogTarget!;
            
            dispatchQueue.sync {
                result = self.logTarget;
            }
            
            return result;
        }
        set {
            dispatchQueue.sync {
                self.logTarget = newValue;
            }
        }
    }
    
    public static func log(message: String, level: Level = .info) {
        dispatchQueue.sync {
            logTarget.log(message: "[\(formattedCurrentDate)][\(level)] \(message)");
        }
    }
    
    public static func info(message: String) {
        log(message: message, level: .info);
    }
    
    public static func warining(message: String) {
        log(message: message, level: .info);
    }
    
    public static func error(message: String) {
        log(message: message, level: .error);
    }
    
    public static func fatal(message: String) {
        log(message: message, level: .fatal);
    }
}

extension Log {
    private static var dateFormatter: DateFormatter = createDateFormatter();
    
    private static func createDateFormatter() -> DateFormatter {
        let formatter: DateFormatter = DateFormatter();
        formatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ssZ";
        
        return formatter;
    }
    
    private static var formattedCurrentDate: String {
        return dateFormatter.string(from: Date());
    }
    
}

public protocol LogTarget {
    func log(message: String);
}

class EmptyLogTarget: LogTarget {
    func log(message: String) {
        // No-Op
    }
}
