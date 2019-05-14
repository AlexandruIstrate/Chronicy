//
//  ExtensionField.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 29/04/2019.
//

import Foundation;

public struct ExtensionField: Equatable, Codable {
    var name: String;
    var type: FieldType;
}

public enum FieldType: String, Codable, Equatable {
    case string;
    case number;
}
