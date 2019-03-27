//
//  InteractionsManager.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 27/03/2019.
//

import Foundation;

public class InteractionsManager {
    
    public static let manager: InteractionsManager = InteractionsManager();
    
    public typealias Interaction = (ModuleTrigger.Key) -> ();
    
    private var triggers: [ModuleTrigger.Key : Interaction] = [:];
    
    private init() {}
    
    public func register(trigger: ModuleTrigger.Key, action: @escaping Interaction) {
        triggers[trigger] = action;
    }
    
    public func raise(trigger: ModuleTrigger.Key) {
        triggers.filter { (arg: (ModuleTrigger.Key, InteractionsManager.Interaction)) -> Bool in
            return trigger == arg.0;
        }.forEach { (arg: (key: ModuleTrigger.Key, value: InteractionsManager.Interaction)) in
            arg.value(arg.key);
        }
    }
}
