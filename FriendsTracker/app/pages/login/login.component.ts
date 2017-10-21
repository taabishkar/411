import { LoginUser } from './../../shared/user/loginUser';
import { UserService } from './../../shared/user/user.service';
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Page } from "ui/page";
import { Router } from "@angular/router";
import { RouterExtensions } from "nativescript-angular/router";
import { Config } from "../../shared/config";
import * as dialogs from "ui/dialogs";

@Component({
    selector: "login",
    providers: [UserService],
    templateUrl: "pages/login/login.html",
    styleUrls: ["pages/login/login-common.css", "pages/login/login.css"]
})

export class LoginComponent implements OnInit {
    loginUser: LoginUser;
    isSigningIn: boolean = false;
    ngOnInit() {
        if (Config.token != "") {
            this.routerExtensions.navigate(["/patients-list"], { clearHistory: true });
        }
        this.page.actionBarHidden = true;
    }

    constructor(private router: Router, private userService: UserService, private page: Page, private routerExtensions: RouterExtensions) {
        this.loginUser = new LoginUser();
    }

    loginCall() {
        this.isSigningIn = true;
        console.log("Login clicked");
        this.login();
    }

    login() {
        this.userService.login(this.loginUser)
            .subscribe(
            () => {
                this.isSigningIn = false;
                this.routerExtensions.navigate(["/patients-list"], { clearHistory: true });
            },
            (error) => {
                this.isSigningIn = false;
                console.log(error);
                dialogs.alert({
                    title: "Error",
                    message: "Invalid Username/Password combination.",
                    okButtonText: "Ok"
                }).then(() => {
                    console.log("Dialog closed!");
                });
            }
            );

    }
}