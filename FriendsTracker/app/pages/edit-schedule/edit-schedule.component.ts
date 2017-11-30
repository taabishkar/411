import { Component, OnInit } from "@angular/core";
import { RouterExtensions } from 'nativescript-angular/router';
import { Router } from '@angular/router';
import * as dialogs from 'ui/dialogs';
import { Schedule, ScheduleViewModel } from './../../pages/schedule/schedule';
import { SelectedIndexChangedEventData } from "nativescript-drop-down";
import { Config } from "./../../shared/config";
import { ScheduleService } from "./../../pages/schedule/schedule.service";
import { TimePicker } from "tns-core-modules/ui/time-picker/time-picker";
import { MyScheduleService } from "./../../shared/user/mySchedule.service";


@Component({
    selector: "edit-schedule",
    providers: [ScheduleService, MyScheduleService],
    templateUrl: "pages/edit-schedule/edit-schedule.html",
    styleUrls: ["pages/edit-schedule/edit-schedule-common.css"]
})

export class EditScheduleComponent implements OnInit {

    public daysIndex :number = 0;
    public days: Array<string>;
    public startTimeIndex : number = 0;
    public startTime: Array<string> = [];
    public endTimeIndex = 0;
    public endTime: Array<string> = [];
    public events: string;
    public mySchedule: Schedule;
    public eventArray: Array<Schedule>=[];
    public userSchedule: ScheduleViewModel;
    public arrayIndex : number;

    constructor(private router: Router, private routerExtensions: RouterExtensions, private scheduleService : ScheduleService, private myScheduleService: MyScheduleService) {
        this.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        this.startTime = this.InitializeStartTime(1,24);
        this.endTime = this.InitializeStartTime(1,24);
    }
    ngOnInit() {
        this.getMySchedule();
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

    public onTap(day, from, to, event) {
        console.log("day is: " + day + " from: " + from + " to: " + to + " Event is: " + event);
        this.mySchedule = new Schedule();
        this.mySchedule.eventId = 0;
        this.mySchedule.from = from + 1;
        this.mySchedule.to = to + 1;
        this.mySchedule.eventDescription = event;
        this.mySchedule.dayId = day;
        this.eventArray[this.arrayIndex] = this.mySchedule;
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

    public onEdit(args) {
    this.daysIndex = args.dayId;
    this.startTimeIndex = args.from;
    this.endTimeIndex = args.to;
    this.events = args.eventDescription;
    this.arrayIndex = this.eventArray.indexOf(args);

    console.log("daysINdex: "+ this.daysIndex);
    console.log("daysINdex: "+ this.startTimeIndex);
    console.log("daysINdex: "+ this.endTimeIndex);
    console.log("daysINdex: "+ this.events);
console.log("testing....................................................");
    console.dir(args);
    }

    public onSave(eventArray: Array<Schedule>){
        this.userSchedule = new ScheduleViewModel();
        this.userSchedule.userId = Config.fromUserId;

        for(var event of eventArray){
            event.eventId = 0;
        }
        this.userSchedule.events = eventArray;
        console.dir(this.userSchedule);
        console.log("UserId: "+ this.userSchedule.userId);
        console.log("UserId: "+ Config.fromUserId);
        this.editSchedule(this.userSchedule);
    }


    public onopen() {
        console.log("Drop Down opened.");
    }

    public onclose() {
        console.log("Drop Down closed.");
    }

     saveSchedule(item) {
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

    getMySchedule() {
        this.myScheduleService.getMySchedule()
            .subscribe(
            (res) => {
                console.log("Schedule fetched!");
                if (res._body != null) {
                    console.dir(res._body.events);
                    this.eventArray = res._body.events;
                    console.log("Schedule: here")
                    console.dir(this.eventArray);
                }
            },
            (error) => {
                console.log(error);
            }
            );
    }

    editSchedule(item) {
        this.scheduleService.editSchedule(item)
            .subscribe(
            (res) => {
               dialogs.alert({
                    title: "Success",
                    message: "Schedule Updated!",
                    okButtonText: "Ok"
                });
            },
            (error) => {
                console.log(error);
            }
            );
    }

}