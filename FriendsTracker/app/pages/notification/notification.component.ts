import { itemsProperty } from 'tns-core-modules/ui/segmented-bar';
import { NotificationService } from './../../shared/user/notification.service';
import { Router } from '@angular/router';
import { RouterExtensions } from 'nativescript-angular/router';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import * as dialogs from 'ui/dialogs';
@Component({
    selector: "notification",
    providers: [NotificationService],
    templateUrl: "pages/notification/notification.html",
    styleUrls: ["pages/notification/notification-common.css"]
})

export class NotificationComponent implements OnInit {
    public Notifications: Notification[];
    public IsFriendRequest: boolean;
    public generalNotification: boolean;
    constructor(private router: Router, private routerExtensions: RouterExtensions, private notificationService: NotificationService) {
        
    }

    ngOnInit(){
        this.getNotifications();
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
    notificationTapped(item){
        console.dir(item);
        if(item.notificationTypeId == 1){
            this.IsFriendRequest = !this.IsFriendRequest;
        }
        else{
            this.generalNotification = !this.generalNotification; 
        }
    }
    acceptRequest(item){
        this.notificationService.acceptRequest(item)
        .subscribe(
            (res) => {
                dialogs.alert({
                    title: "Success",
                    message: "Friend request accepted!",
                    okButtonText: "Ok"
                });
                var index = this.Notifications.indexOf(item);
                this.Notifications.splice(index,1);
                this.IsFriendRequest = !this.IsFriendRequest;
            },
            (error) => {
                dialogs.alert({
                    title: "Error",
                    message: "Sorry, an error occured.",
                    okButtonText: "Ok"
                });
                console.log(error);
            }
        );
    }
    rejectRequest(item){
        this.notificationService.rejectRequest(item)
        .subscribe(
            (res) => {
                dialogs.alert({
                    title: "Success",
                    message: "Friend request rejected!",
                    okButtonText: "Ok"
                });
                var index = this.Notifications.indexOf(item);
                this.Notifications.splice(index,1);
                this.IsFriendRequest = !this.IsFriendRequest;
            },
            (error) => {
                dialogs.alert({
                    title: "Error",
                    message: "Sorry, an error occured.",
                    okButtonText: "Ok"
                });
                console.log(error);
            }
        );
    }
    clearNotification(item){
        this.notificationService.clearNotification(item)
            .subscribe(
                (res) => {
                    
                    var index = this.Notifications.indexOf(item);
                    this.Notifications.splice(index,1);
                    this.generalNotification = !this.generalNotification;
                },
                (error) => {
                    dialogs.alert({
                        title: "Error",
                        message: "Sorry, an error occured.",
                        okButtonText: "Ok"
                    });
                    console.log(error);
                }
            );        
    }
    GoBack(){
        console.log("Back tapped.");
        this.routerExtensions.navigate([""], { clearHistory: true });
    }
} 