import { UserService } from './../../shared/user/user.service';
import { RegisterUser } from './../../shared/user/registerUser';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from "ui/dialogs";

@Component({
    selector: "register",
    providers: [UserService],
    templateUrl: "pages/register/register.html",
    styleUrls: ["pages/register/register-common.css"]
})

export class RegisterComponent implements OnInit {
    registerUser: RegisterUser;
    isSigningUp: boolean = false;
    ngOnInit() {
        this.page.actionBarHidden = true;
        console.log("Inside login");
    }

    constructor(private router: Router, 
        private userService: UserService, 
        private page: Page, 
        private routerExtensions: RouterExtensions) 
    {

        this.registerUser = new RegisterUser();
    }
    
    registerCall(){
        this.isSigningUp = true;
        this.register();
    }

    register() {
        if(this.registerUser.Password != this.registerUser.ConfirmPassword){
            dialogs.alert({
                title: "Error",
                message: "Password and Confirm Password do not match.",
                okButtonText: "Ok"
            }).then(() => {
                console.log("Dialog closed!");
            });
            this.isSigningUp = false;
        } else{
        
        this.userService.register(this.registerUser)
            .subscribe(
            () => {
                this.isSigningUp = false;
                 this.routerExtensions.navigate(["/verification-code"], { clearHistory: true });
            },
            (error) => {
                this.isSigningUp = false;
                console.log(error);
                dialogs.alert({
                    title: "Error",
                    message: "An error was encountered while registering you. Please try again.",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
            }
            );
        }
    }
}