import { SendMood } from './sendMood';
import { UserMood } from './userMood';
import { VerificationCode } from './verificationCode';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Config } from '../config';
import { LoginUser } from './loginUser';
import { RegisterUser } from './registerUser';

@Injectable()
export class MoodService {
    constructor(private http: Http) { }
    username: string;
    getMood() {
        let sendMood = new  SendMood();
        sendMood.UserId = Config.fromUserId;
        let headers = new Headers();
         headers.append("Content-Type", "application/json");
        let body = sendMood;
        return this.http.post(
            Config.apiUrl + 'mood/get',body
        )
         .map((res: Response) => res.json())
            // .do(data => {
            //     Config.token = data.token;
            //     console.log(Config.token);
            //     Config.fromUserId = data.id;
            //     console.log(Config.fromUserId);
            // })
            .catch(this.handleErrors);
    }
    
    setMood(userMood: UserMood){
            console.dir(userMood);
            userMood.userId = Config.fromUserId;
            console.log(Config.apiUrl);
            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            let body = userMood;
            return this.http.post(Config.apiUrl + 'mood/create', 
            body
        )
        .catch(this.handleErrors);
    }
    
    handleErrors(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}