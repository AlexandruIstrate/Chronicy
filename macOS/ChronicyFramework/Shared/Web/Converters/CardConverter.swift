//
//  CardConverter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class CardConverter: Converter<Card, CardModel> {
    private let fieldConverter: CustomFieldConverter = CustomFieldConverter();
    private let tagConverter: TagConverter = TagConverter();
    
    public override var allowsReverseConvert: Bool { return true; }
    
    public override func convert(item: Card) -> CardModel {
        let result: CardModel = CardModel();
//        result.id = item.id;
        result.name = item.name;
        result.comment = item.comment;
        result.date = item.date;
        result.fields = item.fields.map({ (iter: CustomField) -> CustomFieldModel in
            return fieldConverter.convert(item: iter);
        });
        result.tags = item.tags.map({ (iter: CardTag) -> TagModel in
            return tagConverter.convert(item: iter);
        });
        return result;
    }
    
    public override func reverseConvert(item: CardModel) -> Card {
        let result: Card = Card(title: item.name, comment: item.comment, date: item.date);
//        result.id = item.id;
        result.fields = item.fields.map({ (iter: CustomFieldModel) -> CustomField in
            return fieldConverter.reverseConvert(item: iter);
        });
        result.tags = item.tags.map({ (iter: TagModel) -> CardTag in
            return tagConverter.reverseConvert(item: iter);
        });
        return result;
    }
}
