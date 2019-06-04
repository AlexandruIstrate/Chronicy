//
//  CoreDataCustomField+CoreDataProperties.swift
//  ChronicyFrameworkMacOS
//
//  Created by Alexandru Istrate on 03/05/2019.
//
//

import Foundation
import CoreData


extension CoreDataCustomField {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<CoreDataCustomField> {
        return NSFetchRequest<CoreDataCustomField>(entityName: "CoreDataCustomField")
    }

    @NSManaged public var name: String;
    @NSManaged public var value: String?;
    @NSManaged public var type: String;
    @NSManaged public var card: CoreDataCard?;
    @NSManaged public var stack: CoreDataStack?;
    
    var field: CustomField? {
        get {
            guard let actualType: FieldType = FieldType(rawValue: self.type) else {
                Log.error(message: "Could not convert field type name to type");
                return nil;
            }
            
            var ret: CustomField = TextField.instantiate(by: actualType, name: self.name);
    //        ret.displayName = self.displayName;
            ret.value = self.fieldValue(string: self.value);
            ret.type = self.fieldType(string: self.type) ?? .string;
            return ret;
        }
        set {
            
        }
    }
    
    fileprivate func fieldValue(string: String?) -> Any? {
        guard let t: FieldType = self.fieldType(string: self.type) else {
            Log.error(message: "Could not deduce field type!");
            return nil;
        }
        
        switch t {
        case .string:
            return string;
        case .number:
            guard let string: String = string else {
                return nil;
            }
            
            return Float(string);
        }
    }
    
    fileprivate func fieldType(string: String) -> FieldType? {
        return FieldType(rawValue: string);
    }

}

extension CustomField {
    public func field(insert into: NSManagedObjectContext) -> CoreDataCustomField? {
        let f: CoreDataCustomField? = NSEntityDescription.insertNewObject(forEntityName: "CoreDataCustomField", into: into) as? CoreDataCustomField;
        f?.name = self.name;
        f?.type = self.type.rawValue
        f?.value = self.valueAsString();
        return f;
    }
    
    fileprivate func valueAsString() -> String? {
        guard let value: Any = self.value else {
            return nil;
        }
        
        switch self.type {
        case .string:
            return value as? String;
            
        case .number:
            guard let newValue: Float = value as? Float else {
                return nil;
            }
            
            return String(newValue);
        }
    }
}
