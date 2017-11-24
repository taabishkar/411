import { VerificationCode } from './verificationCode';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Config } from '../config';
import { LoginUser } from './loginUser';
import { RegisterUser } from './registerUser';
import { Notification }from './notification';

@Injectable()
export class UserService {
    constructor(private http: Http) { }
    username: string;
    login(user: LoginUser) {
        console.dir(user);
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = user;
        console.log(Config.apiUrl);
        console.log("Hello");
        return this.http.post(
            Config.apiUrl + 'account/login',
            body
        )
            .map((res: Response) => res.json())
            .do(data => {
                console.log(data);
                console.dir(data);
                Config.token = data.token;
                Config.fromUserId = data.id;
                Config.userName = data.userName;
                Config.firstName = data.firstName;
                Config.lastName = data.lastName;
                Config.phoneNumber = data.phoneNumber;
                console.log(Config.phoneNumber);
            })
            .catch(this.handleErrors);
    }
    
    register(user: RegisterUser){
            console.dir(user);
            Config.userName = user.UserName;
            console.log(Config.apiUrl);
            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            let body = user;
            return this.http.post(Config.apiUrl + 'account/register', 
            body
        )
        .catch(this.handleErrors);
    }

    sendVerificationCode(verificationCode: VerificationCode){
        console.dir(verificationCode);
        console.log(Config.apiUrl);
        verificationCode.UserName = Config.userName;
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = verificationCode;
        return this.http.post(Config.apiUrl + 'account/validateAccessCode', 
        body
    )
    .map((res: Response)  => res.json())
    .do(data => {
        console.dir(data);
        Config.token = data.token;
        console.log(Config.token);
        Config.fromUserId = data.id;
        console.log(Config.fromUserId);        
    })
    .catch(this.handleErrors);
}
    handleErrors(error: Response) {
        console.log("Error Occured");
        console.log(error);
        return Observable.throw(error);
    }

}