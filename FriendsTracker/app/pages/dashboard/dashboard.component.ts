import { MoodService } from './../../shared/user/mood.service';
import { UserMood } from './../../shared/user/userMood';
import { UserService } from './../../shared/user/user.service';
import { RegisterUser } from './../../shared/user/registerUser';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from "ui/dialogs";

@Component({
    selector: "dashboard",
    providers: [MoodService],
    templateUrl: "pages/dashboard/dashboard.html",
    styleUrls: ["pages/dashboard/dashboard-common.css"]
})

export class DashBoardComponent implements OnInit {
    public isTapped: boolean;
    public userMood = new UserMood();
    public editClicked: boolean;
    public mood: string;

    constructor(private moodService: MoodService, private router: Router, private routerExtensions: RouterExtensions) {
        this.userMood = new UserMood();
        this.editClicked = false;
    }

    ngOnInit(){
      this.getMood(); 
      this.editClicked = false;
    }
    tapped(){
        console.log("Hamburger tapped");
        this.isTapped = !this.isTapped;
    }

    setMoodCall(){
        this.setMood();
    }
    
    setMood(){
        this.moodService.setMood(this.userMood)
        .subscribe(
            (res) => {
                dialogs.alert({
                    title: "Successful",
                    message: "Your mood has been updated successfully",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
                this.getMood(); 
                this.editClicked = false;
            },
            (error) => {
                dialogs.alert({
                    title: "Error",
                    message: "An error occured",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
            }
            );
    }

    getMood(){
        this.moodService.getMood()
        .subscribe(
            (res) => {
                this.userMood = res;
                console.dir(this.userMood);
                this.mood = res.moodDescription;
                console.log(this.mood);
            },
            (error) => {
                
            }
            );
    }

    editMood(){
        this.editClicked = true;
    }

}