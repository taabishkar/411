import { Config } from './../../shared/config';
import { WeekDays } from './../../shared/WeekDays';
import { Schedule } from './../schedule/schedule';
import { itemsProperty } from 'tns-core-modules/ui/segmented-bar';
import { Router } from '@angular/router';
import { RouterExtensions } from 'nativescript-angular/router';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import * as dialogs from 'ui/dialogs';
import { MyScheduleService } from './../../shared/user/mySchedule.service';
@Component({
    selector: "friend-details",
    providers: [MyScheduleService],
    templateUrl: "pages/friend-details/friend-details.html",
    styleUrls: ["pages/friend-details/friend-details-common.css"]
})

export class FriendDetailsComponent implements OnInit {
    WeekDays = WeekDays;
    public schedule: Schedule[] = [];
    public mood: string = '';
    public friendName: string = '';
    constructor(private router: Router, private routerExtensions: RouterExtensions, private myScheduleService: MyScheduleService) {
    }

    ngOnInit() {
        this.friendName= Config.currentFriendFullName;
        this.getFriendsScheduleAndMood(Config.currentFriendId);
    }
    getFriendsScheduleAndMood(friendsUserId) {
        this.myScheduleService.getFriendsScheduleAndMood(friendsUserId)
            .subscribe(
            (res) => {
                console.log("Schedule fetched!");
                if (res._body != null) {
                    console.dir(res._body.schedule.events);
                    this.schedule = res._body.schedule.events;
                    this.mood = res._body.mood.moodDescription;
                    console.log("Schedule: here")
                    console.dir(this.schedule);
                    console.log("Mood: " + this.mood);
                }
            },
            (error) => {
                console.log(error);
            }
            );
    }
    GoBack() {
        console.log("Back tapped.");
        this.routerExtensions.navigate(["friends"], { clearHistory: true });
    }
} 