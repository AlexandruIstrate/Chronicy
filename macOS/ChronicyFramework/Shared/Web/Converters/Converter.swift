//
//  Converter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class Converter<TInput, TOutput> {
    public var allowsReverseConvert: Bool { return false; }
    
    public func convert(item: TInput) -> TOutput { fatalError("Converter is an abstract class"); }
    public func reverseConvert(item: TOutput) -> TInput { fatalError("Converter is an abstract class"); }
}
