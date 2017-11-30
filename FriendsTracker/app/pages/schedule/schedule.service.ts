
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { ScheduleViewModel } from "./../../pages/schedule/schedule";
import { Config } from "./../../shared/config";

@Injectable()
export class ScheduleService {
    constructor(private http: Http) { }    
     saveSchedule(item: ScheduleViewModel){       
        let headers = new Headers();
         headers.append("Content-Type", "application/json");       
        let body = item;
        return this.http.post(
            Config.apiUrl + 'Schedule/save', body
        )
        .catch(this.handleErrors);
     }     

    handleErrors(error: Response) {
        console.log(error + 'Randakhal');
        return Observable.throw(error);
    }

editSchedule(item: ScheduleViewModel){       
        let headers = new Headers();
         headers.append("Content-Type", "application/json");       
        let body = item;
        return this.http.post(
            Config.apiUrl + 'Schedule/update', body
        )
        .catch(this.handleErrors);
     }     

    


}