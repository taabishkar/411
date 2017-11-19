import { AddFriend } from './../../shared/friends/add-friend';
import { SearchFriendsResponse } from './../../shared/friends/search-friends-response';

import { FriendService } from './../../shared/friends/friend.service';
import { SearchFriends } from './../../shared/friends/search-friends';
import { UserService } from './../../shared/user/user.service';
import { RegisterUser } from './../../shared/user/registerUser';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from "ui/dialogs";

@Component({
    selector: "search-page",
    providers: [FriendService],
    templateUrl: "pages/search-page/search-page.html",
    styleUrls: ["pages/search-page/search-page-common.css"]
})

export class SearchPageComponent implements OnInit {
    public searchfriends = new SearchFriends();
    public searchFriendsResponse = new SearchFriendsResponse();
    public fullname: string;
    public addfriend = new AddFriend();
    public isWorking: boolean = false;
    public responseReceived : boolean = false;
    ngOnInit(){

    }

    constructor(private router: Router, private friendService: FriendService, private page: Page, private routerExtensions: RouterExtensions) {
        this.searchfriends = new SearchFriends();
    }

    searchFriendsCall(){
       this.isWorking = true;
       this.searchfriends.UserId = Config.fromUserId;
       console.dir(this.searchfriends);
       this.searchFriends();
    }

    
        searchFriends() {
            this.friendService.search(this.searchfriends)
                .subscribe(
                (res) => {
                    this.responseReceived = true;
                    this.isWorking = false;
                   console.dir(res);
                   this.searchFriendsResponse = res;
                   this.fullname = this.searchFriendsResponse.firstName + " "+ this.searchFriendsResponse.lastName;
                   Config.toUserId = res.id;
                },
                (error) => {
                    this.isWorking = false;
                    console.log(error);
                    dialogs.alert({
                        title: "Error",
                        message: "Invalid Username/Password combination.",
                        okButtonText: "Ok"
                    }).then(() => {
                        console.log("Dialog closed!");
                    });
                }
                );
    
        }

        addFriendCall(){
            this.isWorking = true;
            this.addFriend();
            
        }

        addFriend(){
            this.addfriend.FromUserId = Config.fromUserId;
            this.addfriend.ToUserId = Config.toUserId;
            this.friendService.addFriend(this.addfriend)
            .subscribe(
            (res) => {
                this.isWorking = false;
               dialogs.alert({
                title: "Success",
                message: "Your request has been sent.",
                okButtonText: "Ok"
            }).then(() => {
                console.log("Dialog closed!");
            });
            },
            (error) => {
                this.isWorking = false;
                console.log(error);
                dialogs.alert({
                    title: "Error",
                    message: "An Error Occured. Please try again later.",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
            }
            );

        }

        GoBack(){
            console.log("Back tapped.");
            this.routerExtensions.navigate([""], { clearHistory: true });
        }
    

}