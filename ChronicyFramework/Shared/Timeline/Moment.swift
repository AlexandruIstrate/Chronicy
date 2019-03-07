//
//  Moment.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Foundation;

public class Moment {
    public var momentData: MomentData = MomentData();
}

public struct MomentData {
    var data: [MomentDataKey : Any] = [:];
}

public enum MomentDataKey: Hashable {
    
}
