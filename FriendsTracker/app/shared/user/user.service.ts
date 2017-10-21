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
export class UserService {
    constructor(private http: Http) { }
    username: string;
    login(user: LoginUser) {
        console.dir(user);
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = user;
        return this.http.post(
            Config.apiUrl + 'account/login',
            body
        )
            .map((res: Response) => res.json())
            .do(data => {
                Config.token = data.Token;
                console.log(Config.token);
            })
            .catch(this.handleErrors);
    }
    
    register(user: RegisterUser){
            console.dir(user);
            Config.UserName = user.UserName;
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
        verificationCode.UserName = Config.UserName;
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = verificationCode;
        return this.http.post(Config.apiUrl + 'account/validateAccessCode', 
        body
    )
    .map((res: Response)  => res.json())
    .do(data => {
        Config.token = data.token;
        console.log(Config.token);
    })
    .catch(this.handleErrors);
}
    handleErrors(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}