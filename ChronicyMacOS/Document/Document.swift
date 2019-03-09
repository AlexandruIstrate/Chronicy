//
//  Document.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 07/03/2019.
//

import Cocoa

class Document: NSDocument {
    
    private var content = Content(name: "Default");
    private var contentViewController: ViewController!;
    
    // MARK: - Enablers
    
    // This enables auto save.
    override class var autosavesInPlace: Bool {
        return true
    }
    
    // This enables asynchronous-writing.
    override func canAsynchronouslyWrite(to url: URL, ofType typeName: String, for saveOperation: NSDocument.SaveOperationType) -> Bool {
        return true
    }
    
    // This enables asynchronous reading.
    override class func canConcurrentlyReadDocuments(ofType: String) -> Bool {
//        return ofType == "public.plain-text";
        return true;
    }
    
    // MARK: - User Interface
    
    override func makeWindowControllers() {
        // Returns the storyboard that contains your document window.
        let storyboard: NSStoryboard = NSStoryboard(name: NSStoryboard.Name("Main"), bundle: nil);
        
        guard let windowController: NSWindowController =
            storyboard.instantiateController(
                withIdentifier: NSStoryboard.SceneIdentifier("Document Window Controller")) as? NSWindowController else {
            return;
        }
        
        addWindowController(windowController)
        
        // Set the view controller's represented object as your document.
        guard let contentVC: ViewController = windowController.contentViewController as? ViewController else {
            return;
        }
        
        contentVC.representedObject = content;
        contentViewController = contentVC;
    }
    
    // MARK: - Reading and Writing
    
    override func read(from data: Data, ofType typeName: String) throws {
        content.read(from: data)
    }
    
    override func data(ofType typeName: String) throws -> Data {
        return content.data()!
    }
    
}
