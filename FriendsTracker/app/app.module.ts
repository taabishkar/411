import { VerificationCodeComponent } from './pages/verification-code/verification-code.component';
import { RegisterComponent } from './pages/register/register.component';
import { NgModule } from "@angular/core";
import { NativeScriptModule } from "nativescript-angular/nativescript.module";
import { NativeScriptFormsModule } from "nativescript-angular/forms";
import { NativeScriptHttpModule } from "nativescript-angular/http";
import { NativeScriptRouterModule } from "nativescript-angular/router";
import { AccordionModule } from "nativescript-accordion/angular";
import { DropDownModule  } from "nativescript-drop-down/angular";


import { AppComponent } from "./app.component";
import { routes, navigatableComponents } from "./app.routing";
import { LoginComponent } from "./pages/login/login.component";

@NgModule({
  imports: [
    NativeScriptModule,
    NativeScriptFormsModule,
    NativeScriptHttpModule,
    NativeScriptRouterModule,
    AccordionModule,
    DropDownModule,
    NativeScriptRouterModule.forRoot(routes)
  ],

  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    VerificationCodeComponent
  ],

  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
