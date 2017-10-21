
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { VerificationCodeComponent } from './pages/verification-code/verification-code.component';

export const routes = [
  { path: "", component: LoginComponent }, 
  {path: "register", component: RegisterComponent},
  {path: "verification-code", component: VerificationCodeComponent}
];

export const navigatableComponents = [
  LoginComponent,
  RegisterComponent,
  VerificationCodeComponent
];