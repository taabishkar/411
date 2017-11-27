import { Component, OnInit } from "@angular/core";
import { RouterExtensions } from 'nativescript-angular/router';
import { Router } from '@angular/router';
import * as dialogs from 'ui/dialogs';
import { Schedule, ScheduleViewModel } from './../../pages/schedule/schedule';
import { SelectedIndexChangedEventData } from "nativescript-drop-down";
import { Config } from "./../../shared/config";
import { ScheduleService } from "./../../pages/schedule/schedule.service";
import { TimePicker } from "tns-core-modules/ui/time-picker/time-picker";


@Component({
    selector: "schedule",
    providers: [ScheduleService],
    templateUrl: "pages/schedule/schedule.html",
    styleUrls: ["pages/schedule/schedule.common.css"]
})

export class ScheduleComponent implements OnInit {

    public daysIndex = 0;
    public days: Array<string>;
    public startTimeIndex = 0;
    public startTime: Array<string> = [];
    public endTimeIndex = 0;
    public endTime: Array<string> = [];
    public events: String;
    public mySchedule: Schedule;
    public eventArray: Array<Schedule>=[];
    public userSchedule: ScheduleViewModel;

    constructor(private router: Router, private routerExtensions: RouterExtensions, private scheduleService : ScheduleService) {
        this.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        this.startTime = this.InitializeStartTime(1,24);
        this.endTime = this.InitializeStartTime(1,24);
    }
    ngOnInit() {
    }

   InitializeStartTime(startIndex: number, endIndex:number){
       var result = new Array<string>();
       for (var i = startIndex; i <= endIndex; i++){
            result.push("  "+ i + "  ");
       }
        return result;
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
        this.mySchedule.EventDescription = event;
        this.mySchedule.DayId = day;
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
        this.userSchedule.events = eventArray;
        console.dir(this.userSchedule);
        console.log("UserId: "+ this.userSchedule.userId);
        console.log("UserId: "+ Config.fromUserId);
        this.getNotifications(this.userSchedule);
    }


    public onopen() {
        console.log("Drop Down opened.");
    }

    public onclose() {
        console.log("Drop Down closed.");
    }

     getNotifications(item) {
        this.scheduleService.saveSchedule(item)
            .subscribe(
            (res) => {   
               dialogs.alert({
                    title: "Success",
                    message: "Schedule Saved!",
                    okButtonText: "Ok"
                });         
            },
            (error) => {
                console.log(error);
            }
            );
    }
}