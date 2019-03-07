//
//  Timeline.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;

public class Timeline {
    public var name: String;
    public var moments: [Moment] = [];
    
    init(name: String) {
        self.name = name;
    }
}
