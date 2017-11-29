import { SendMood } from './sendMood';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Config } from '../config';

@Injectable()
export class MyScheduleService {
    constructor(private http: Http) { }    
    getMySchedule() {
        let userId = new SendMood();
        userId.UserId = Config.fromUserId;
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
        let body = userId;
        return this.http.post(
            Config.apiUrl + 'schedule/get',body
        )
        .catch(this.handleErrors);
    }  
    getFriendsScheduleAndMood(friendsUserId) {
        let userId = new SendMood();
        userId.UserId = friendsUserId;
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
        let body = userId;
        return this.http.post(
            Config.apiUrl + 'schedule/get/friend',body
        )
        .catch(this.handleErrors);
    }            
    handleErrors(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}