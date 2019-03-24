//
//  Action.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 24/03/2019.
//

import Foundation;

public class Action {
    public var name: String;
    public var comment: String;
    public var date: Date;
    
    public init(name: String, comment: String = "", date: Date = Date()) {
        self.name = name;
        self.comment = comment;
        self.date = date;
    }
}

extension Action: Equatable {
    public static func == (lhs: Action, rhs: Action) -> Bool {
        return lhs.name     == rhs.name &&
            lhs.comment  == rhs.comment &&
            lhs.date     == rhs.date;
    }
}
