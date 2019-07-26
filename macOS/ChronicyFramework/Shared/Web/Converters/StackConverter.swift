//
//  StackConverter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class StackConverter: Converter<Stack, StackModel> {
    private let fieldConverter: CustomFieldConverter = CustomFieldConverter();
    private let cardConverter: CardConverter = CardConverter();
    
    public override var allowsReverseConvert: Bool { return true; }
    
    public override func convert(item: Stack) -> StackModel {
        let result: StackModel = StackModel();
//        result.id = item.id;
        result.name = item.name;
        result.fields = item.inputTemplate.fields.map({ (iter: CustomField) -> CustomFieldModel in
            return fieldConverter.convert(item: iter);
        });
        result.cards = item.cards.map({ (iter: Card) -> CardModel in
            return cardConverter.convert(item: iter);
        })
        return result;
    }
    
    public override func reverseConvert(item: StackModel) -> Stack {
        let result: Stack = Stack(name: item.name);
//        result.id = item.id;
        result.inputTemplate.fields = item.fields.map({ (iter: CustomFieldModel) -> CustomField in
            return fieldConverter.reverseConvert(item: iter);
        });
        result.cards = item.cards.map({ (iter: CardModel) -> Card in
            return cardConverter.reverseConvert(item: iter);
        })
        return result;
    }
}
