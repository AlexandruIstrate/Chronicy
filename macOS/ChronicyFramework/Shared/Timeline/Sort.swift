//
//  Sort.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 18/05/2019.
//

import Foundation;

public func stackSort(lhs: Stack, rhs: Stack) -> Bool {
    return lhs.name < rhs.name;
}

public func cardSort(lhs: Card, rhs: Card) -> Bool {
    return lhs.date > rhs.date;
}
