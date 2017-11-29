import { Config } from './../../shared/config';
import { Friend } from './../../shared/friends/friend';
import { itemsProperty } from 'tns-core-modules/ui/segmented-bar';
import { Router } from '@angular/router';
import { RouterExtensions } from 'nativescript-angular/router';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import * as dialogs from 'ui/dialogs';
import { FriendsService } from '../../shared/user/friends.service';
@Component({
    selector: "friends",
    providers: [FriendsService],
    templateUrl: "pages/friends/friends.html",
    styleUrls: ["pages/friends/friends-common.css"]
})

export class FriendsComponent implements OnInit {
    public friends: Friend[] = [];
    constructor(private router: Router, private routerExtensions: RouterExtensions, private friendsService: FriendsService) {
        
    }

    ngOnInit(){
        this.getAllFriends();
    }
    getAllFriends(){
        this.friendsService.getAllFriends()
        .subscribe(
            (res) => {
                console.log("Friends fetched!");
                if(res != null){
                    console.dir(res);
                    this.friends = res._body ;
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }
    ShowFriendsDetail(userId, fullName){
        Config.currentFriendId = userId;
        Config.currentFriendFullName = fullName;
        this.routerExtensions.navigate(["friend-details"]);
    }
    GoBack(){
        console.log("Back tapped.");
        this.routerExtensions.navigate([""], { clearHistory: true });
    }
} 