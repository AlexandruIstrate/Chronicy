//
//  InformationBroadcaster.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 29/04/2019.
//

import Foundation;

public class InformationBroadcaster {
    public init() {
        
    }
    
    public func broadcastNotebooks(notebooks: [Notebook]) {
        DistributedObjectManager.manager.set(object: notebooks.map({ (iter: Notebook) -> ExtensionNotebook in
            return iter.toExtensionType();
        }), for: SharedConstants.DistributedObjectKeys.notebooks);
    }
}

extension CustomField {
    fileprivate func toExtensionType() -> ExtensionField {
        return ExtensionField(name: self.name, type: self.type);
    }
}

extension Stack {
    fileprivate func toExtensionType() -> ExtensionStack {
        return ExtensionStack(name: self.name, fields: self.inputTemplate.fields.map({ (iter: CustomField) -> ExtensionField in
            return iter.toExtensionType();
        }))
    }
}

extension Notebook {
    fileprivate func toExtensionType() -> ExtensionNotebook {
        return ExtensionNotebook(name: self.name, stacks: self.stacks.map({ (iter: Stack) -> ExtensionStack in
            return iter.toExtensionType();
        }));
    }
}
