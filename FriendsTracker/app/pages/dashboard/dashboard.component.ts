import { forEach } from '@angular/router/src/utils/collection';
import { DashboardItems } from './../../shared/dashboard/dashboard';
import { MoodService } from './../../shared/user/mood.service';
import { UserMood } from './../../shared/user/userMood';
import { UserService } from './../../shared/user/user.service';
import { RegisterUser } from './../../shared/user/registerUser';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from 'ui/dialogs';
import { NotificationService } from './../../shared/user/notification.service';

@Component({
    selector: "dashboard",
    providers: [MoodService, NotificationService],
    templateUrl: "pages/dashboard/dashboard.html",
    styleUrls: ["pages/dashboard/dashboard-common.css"]
})

export class DashBoardComponent implements OnInit {
    public isTapped: boolean;
    public userMood = new UserMood();
    public editClicked: boolean;
    public mood: string;
    public username: string;
    public firstname: string;
    public lastname: string;
    public phonenumber: string;
    public dashboardItem = new DashboardItems();
    public moodAction: string;
    public Notifications: Notification[] = [];

    constructor(private moodService: MoodService, private router: Router, private routerExtensions: RouterExtensions, private notificationService: NotificationService) {
        this.userMood = new UserMood();
        this.editClicked = false;
        this.dashboardItem = new DashboardItems();
    }

    ngOnInit(){
      this.getMood(); 
      this.editClicked = false;
      this.setDashBoardData();
      this.getNotifications();
    }
    tapped(){
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
                this.moodAction = "Change Mood";
                console.log(this.mood);
            },
            (error) => {
                this.moodAction = "Set Mood";
            }
            );
    }

    editMood(){
        this.editClicked = true;
    }

    setDashBoardData(){
        this.dashboardItem.firstname = Config.firstName;
        this.dashboardItem.lastname = Config.lastName;
        this.dashboardItem.phonenumber = Config.phoneNumber;
        this.dashboardItem.username = Config.userName;
    }

    getNotifications(){
        this.notificationService.getNotifications()
        .subscribe(
            (res) => {
                console.log("Notifications fetched!");
                if(res._body != null){
                    this.Notifications = res._body;
                }
                console.dir(this.Notifications);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    SignOut(){
        Config.token = "";
        Config.userName ="";
        Config.phoneNumber ="";
        Config.firstName ="";
        Config.lastName="";
        Config.fromUserId="";
        Config.toUserId="";
        this.routerExtensions.navigate([""], { clearHistory: true });
    }

}
