import { WeekDays } from './../../shared/WeekDays';
import { Schedule } from './../schedule/schedule';
import { itemsProperty } from 'tns-core-modules/ui/segmented-bar';
import { Router } from '@angular/router';
import { RouterExtensions } from 'nativescript-angular/router';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import * as dialogs from 'ui/dialogs';
import { MyScheduleService } from './../../shared/user/mySchedule.service';
@Component({
    selector: "my-schedule",
    providers: [MyScheduleService],
    templateUrl: "pages/my-schedule/my-schedule.html",
    styleUrls: ["pages/my-schedule/my-schedule-common.css"]
})

export class MyScheduleComponent implements OnInit {
    WeekDays = WeekDays;
    public schedule: Schedule[] = [];
    constructor(private router: Router, private routerExtensions: RouterExtensions, private myScheduleService: MyScheduleService) {
    }

    ngOnInit() {
        this.getMySchedule();
    }
    getMySchedule() {
        this.myScheduleService.getMySchedule()
            .subscribe(
            (res) => {
                console.log("Schedule fetched!");
                if (res._body != null) {
                    console.dir(res._body.events);
                    this.schedule = res._body.events;
                    console.log("Schedule: here")
                    console.dir(this.schedule);
                }
            },
            (error) => {
                console.log(error);
            }
            );
    }
    GoBack() {
        console.log("Back tapped.");
        this.routerExtensions.navigate([""], { clearHistory: true });
    }
} 