//
//  View.swift
//  ChronicyMacOS
//
//  Created by Alexandru Istrate on 20/03/2019.
//

import Cocoa;

protocol CustomOperationSeparatable {
    func onLoadData();
    func onLayoutView();
}

protocol ViewInteractionDelegate {
    func onClick(at point: CGPoint);
    func onRightClick(at point: CGPoint);
}
