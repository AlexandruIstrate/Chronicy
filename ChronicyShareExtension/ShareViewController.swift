//
//  ShareViewController.swift
//  ChronicyShareExtension
//
//  Created by Alexandru Istrate on 28/03/2019.
//

import Cocoa
import Social

class ShareViewController: SLComposeServiceViewController {

    override func loadView() {
        super.loadView()
    
        // Insert code here to customize the view
        self.title = NSLocalizedString("ChronicyShareExtension", comment: "Title of the Social Service")
    
        NSLog("Input Items = %@", self.extensionContext!.inputItems as NSArray)
    }

    override func didSelectPost() {
        // Perform the post operation
        // When the operation is complete (probably asynchronously), the service should notify the success or failure as well as the items that were actually shared
    
        let inputItem = self.extensionContext!.inputItems[0] as! NSExtensionItem
    
        let outputItem: NSExtensionItem = inputItem.copy() as! NSExtensionItem;
        outputItem.attributedContentText = NSAttributedString(string: self.contentText);
        // Complete implementation by setting the appropriate value on the output item
        
        for item: NSItemProvider in (outputItem.attachments ?? []) {
            print("BEGIN");
            getTextData(itemProvider: item);
            print("END");
        }
    
        let outputItems: [Any] = [outputItem];
    
        self.extensionContext!.completeRequest(returningItems: outputItems, completionHandler: nil)
    }

    override func didSelectCancel() {
        // Cleanup
    
        // Notify the Service was cancelled
        let cancelError = NSError(domain: NSCocoaErrorDomain, code: NSUserCancelledError, userInfo: nil)
        self.extensionContext!.cancelRequest(withError: cancelError)
    }

    override func isContentValid() -> Bool {
        let messageLength = self.contentText.trimmingCharacters(in: CharacterSet.whitespaces).utf8.count
        let charactersRemaining = 140 - messageLength
        self.charactersRemaining = charactersRemaining as NSNumber
        
        if charactersRemaining >= 0 {
            return true
        }
        
        return false
    }

}

extension ShareViewController {
    private func getTextData(itemProvider: NSItemProvider) -> String? {
        let typeIdentifier: String = kUTTypeText as! String;
        
        var result: String?;
        
        if itemProvider.hasItemConformingToTypeIdentifier(typeIdentifier) {
            itemProvider.loadItem(forTypeIdentifier: typeIdentifier, options: nil) { (data: NSSecureCoding?, error: Error?) in
                guard error == nil else {
                    return;
                }
                
                if let content = data {
                    let newData: NSData = NSData(contentsOf: content as! URL)!;
                    
                    guard let d: Data = newData as Data else {
                        return;
                    }
                    
                    print(String(data: d, encoding: .utf8));
                }
//                let str: String = String(data: data!, encoding: .utf8);
            }
        }
        
        return result;
    }
}
