//
//  NotebookConverter.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class NotebookConverter: Converter<Notebook, NotebookModel> {
    private let stackConverter: StackConverter = StackConverter();
    
    public override var allowsReverseConvert: Bool { return true; }
    
    public override func convert(item: Notebook) -> NotebookModel {
        let result: NotebookModel = NotebookModel();
        result.id = item.id;
        result.name = item.name;
        result.stacks = item.stacks.map({ (iter: Stack) -> StackModel in
            return stackConverter.convert(item: iter);
        });
        return result;
    }
    
    public override func reverseConvert(item: NotebookModel) -> Notebook {
        let result: Notebook = Notebook(name: item.name);
        result.id = item.id;
        result.stacks = item.stacks.map({ (iter: StackModel) -> Stack in
            return stackConverter.reverseConvert(item: iter);
        });
        return result;
    }

}

public extension Notebook {
    var webModel: NotebookModel {
        let converter: NotebookConverter = NotebookConverter();
        return converter.convert(item: self);
    }
}

public extension NotebookModel {
    var notebook: Notebook {
        let converter: NotebookConverter = NotebookConverter();
        return converter.reverseConvert(item: self);
    }
}
