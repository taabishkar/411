import { SendMood } from './sendMood';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Config } from '../config';

@Injectable()
export class NotificationService {
    constructor(private http: Http) { }    
    getNotifications() {
        let userId = new SendMood();
        userId.UserId = Config.fromUserId;
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
        let body = userId;
        return this.http.post(
            Config.apiUrl + 'notification/get',body
        )
        .catch(this.handleErrors);
    }
            
    handleErrors(error: Response) {
        console.log(error + 'Randakhal');
        return Observable.throw(error);
    }

}