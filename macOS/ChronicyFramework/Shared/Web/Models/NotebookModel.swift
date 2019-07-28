//
//  NotebookModel.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 25/07/2019.
//

import Foundation;

public class NotebookModel: ModelBase {
    // ModelBase Start
    
    public var errorCode: Int = 0;
    public var errorMessage: String?;
    
    public var statusCode: Int = 200;
    
    // ModelBase End
    
    public var id: Int = 0;
    public var name: String = "";
    
    public var stacks: [StackModel] = [];
}
