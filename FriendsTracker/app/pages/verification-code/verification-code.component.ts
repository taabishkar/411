import { VerificationCode } from './../../shared/user/verificationCode';
import { UserService } from './../../shared/user/user.service';
import { RegisterUser } from './../../shared/user/registerUser';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from "ui/dialogs";

@Component({
    selector: "verification-code",
    providers: [UserService],
    templateUrl: "pages/verification-code/verification-code.html",
    styleUrls: ["pages/verification-code/verification-code-common.css"]
})

export class VerificationCodeComponent implements OnInit {
    verificationcode: VerificationCode;
    isRequesting: boolean = false;
    ngOnInit() {
        this.page.actionBarHidden = true;
    }

    constructor(private router: Router, 
        private userService: UserService, 
        private page: Page, 
        private routerExtensions: RouterExtensions) 
    {
        this.verificationcode = new VerificationCode();
    }
    
    sendVerificationCodeCall(){
            this.isRequesting = true;
            this.sendVerificationCode();

    }
    sendVerificationCode() {
        this.userService.sendVerificationCode(this.verificationcode)
            .subscribe(
            () => {
                this.isRequesting = false;
                 this.routerExtensions.navigate(["/patients-list"], { clearHistory: true });
            },
            (error) => {
                this.isRequesting = false;
                console.log(error);
                dialogs.alert({
                    title: "Error",
                    message: "Entered code could not be verified. Please try again.",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
            }
            );

    }
}