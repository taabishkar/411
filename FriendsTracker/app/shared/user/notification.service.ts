import { SendMood } from './sendMood';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Config } from '../config';
import { FriendRequestResponse } from './notification';

@Injectable()
export class NotificationService {
    public friendRequestResponse = new FriendRequestResponse();
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
     acceptRequest(item){
        
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
         this.friendRequestResponse.notification = item;
         this.friendRequestResponse.LoggedInUserId = Config.fromUserId;
        let body = this.friendRequestResponse;
        return this.http.post(
            Config.apiUrl + 'friends/accept',body
        )
        .catch(this.handleErrors);
     }     
     rejectRequest(item){
        
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
         this.friendRequestResponse.notification = item;
         this.friendRequestResponse.LoggedInUserId = Config.fromUserId;
        let body = this.friendRequestResponse;
        return this.http.post(
            Config.apiUrl + 'friends/reject',body
        )
        .catch(this.handleErrors);
     }
     clearNotification(item){
        
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
         let body = item;
        return this.http.post(
            Config.apiUrl + 'notification/acknowledge',body
        )
        .catch(this.handleErrors);
     }       
    handleErrors(error: Response) {
        console.log(error + 'Randakhal');
        return Observable.throw(error);
    }

}