//
//  DetailsCell.swift
//  ChronicyiOS
//
//  Created by Alexandru Istrate on 02/04/2019.
//

import UIKit;
import FoldingCell;

class DetailsCell: FoldingCell {
    
    override func awakeFromNib() {
        foregroundView.layer.cornerRadius = 10;
        foregroundView.layer.masksToBounds = true;
        super.awakeFromNib();
    }
    
    override func animationDuration(_ itemIndex: NSInteger, type: FoldingCell.AnimationType) -> TimeInterval {
        let durations: [TimeInterval] = [0.26, 0.2, 0.2];
        return durations[itemIndex];
    }
    
}
