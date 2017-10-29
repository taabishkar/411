import { AddFriend } from './add-friend';
import { SearchFriends } from './search-friends';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Config } from '../config';

@Injectable()
export class FriendService {
    constructor(private http: Http) { }
    username: string;
    search(friend: SearchFriends) {
        console.dir(friend);
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = friend;
        return this.http.post(
            Config.apiUrl + 'friends/search',
            body
        )
            .map((res: Response) => res.json())
            .do(data => {
                console.dir(data);
            })
            .catch(this.handleErrors);
    }

    addFriend(addFriend: AddFriend) {
        console.dir(addFriend);
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        let body = addFriend;
        return this.http.post(
            Config.apiUrl + 'friends/addfriend',
            body
        )
            .catch(this.handleErrors);
    }

    handleErrors(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}