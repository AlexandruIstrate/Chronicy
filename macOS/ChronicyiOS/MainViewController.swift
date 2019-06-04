//
//  MainViewController.swift
//  ChronicyiOS
//
//  Created by Alexandru Istrate on 17/04/2019.
//

import UIKit;
import Cards;

class MainViewController: UITableViewController {

    override func viewDidLoad() {
        super.viewDidLoad();
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 10;
    }
    
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell: UITableViewCell = tableView.dequeueReusableCell(withIdentifier: "Cell", for: indexPath);
        let card: CardArticle = cell.viewWithTag(1000) as! CardArticle;
        
        let cardContent: UIViewController = storyboard!.instantiateViewController(withIdentifier: "CardContent");
        card.shouldPresent(cardContent, from: self, fullscreen: true);
        
        return cell;
    }
}
