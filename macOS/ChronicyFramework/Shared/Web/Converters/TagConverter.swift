//
//  TagConverter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class TagConverter: Converter<CardTag, TagModel> {
    public override var allowsReverseConvert: Bool { return true; }
    
    public override func convert(item: CardTag) -> TagModel {
        let result: TagModel = TagModel();
//        result.id = item.id;
        result.name = item.title;
        result.description = item.description ?? "";
        
        return result;
    }
    
    public override func reverseConvert(item: TagModel) -> CardTag {
        return CardTag(title: item.name, description: item.description, color: CGColor.white);
    }
}
