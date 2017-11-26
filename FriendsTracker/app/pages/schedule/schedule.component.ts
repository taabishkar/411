import { Component, OnInit } from "@angular/core";
import { RouterExtensions } from 'nativescript-angular/router';
import { Router } from '@angular/router';
import * as dialogs from 'ui/dialogs';
import { Schedule, ScheduleViewModel } from './../../pages/schedule/schedule';
import { SelectedIndexChangedEventData } from "nativescript-drop-down";
import { Config } from "./../../shared/config";


@Component({
    selector: "schedule",
    providers: [],
    templateUrl: "pages/schedule/schedule.html",
    styleUrls: ["pages/schedule/schedule.common.css"]
})

export class ScheduleComponent implements OnInit {

    public daysIndex = 0;
    public days: Array<string>;
    public startTimeIndex = 0;
    public startTime: Array<Number>;
    public endTimeIndex = 0;
    public endTime: Array<Number>;
    public events: String;
    public mySchedule: Schedule;
    public eventArray: Array<Schedule>=[];
    public userSchedule: ScheduleViewModel;

    constructor(private router: Router, private routerExtensions: RouterExtensions) {
        this.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        this.startTime = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24];
        this.endTime = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24];
    }
    ngOnInit() {
    }

    GoBack() {
        console.log("Back tapped.");
        this.routerExtensions.navigate(["dashboard"], { clearHistory: true });
    }

    public onchangeDays(args: SelectedIndexChangedEventData) {
        console.log(`Drop Down selected index changed from ${args.oldIndex} to ${args.newIndex}`);
    }

    public onchangeStartTime(args: SelectedIndexChangedEventData) {

        console.log(`Drop Down selected index changed from ${args.oldIndex} to ${args.newIndex}`);

    }

    public onchangeEndTime(args: SelectedIndexChangedEventData) {
        console.log(`Drop Down selected index changed from ${args.oldIndex} to ${args.newIndex}`);
    }

    public onTap(day: number, from: number, to: number, event: String) {
        console.log("day is: " + day + " from: " + from + " to: " + to + " Event is: " + event);
        this.mySchedule = new Schedule();
        this.mySchedule.EventId = 0;
        this.mySchedule.From = from + 1;
        this.mySchedule.To = to + 1;
        this.mySchedule.Event = event;
        this.mySchedule.Day = this.days[day];
        this.eventArray.push(this.mySchedule);
        this.reset();
        console.dir(this.mySchedule);
        console.dir(this.eventArray);
    }
    public reset() {
        this.daysIndex = 0;
        this.endTimeIndex = 0;
        this.startTimeIndex = 0;
        this.events = "";
    }
    public onRemove(args: Schedule) {
        var index = this.eventArray.indexOf(args);
        this.eventArray.splice(index, 1);
        console.dir(this.eventArray);
    }

    public onSave(eventArray: Array<Schedule>){
        this.userSchedule = new ScheduleViewModel();
        this.userSchedule.userId = Config.fromUserId;
        this.userSchedule.scheduleArray = eventArray;
        console.dir(this.userSchedule);
    }

    public onopen() {
        console.log("Drop Down opened.");
    }

    public onclose() {
        console.log("Drop Down closed.");
    }
}