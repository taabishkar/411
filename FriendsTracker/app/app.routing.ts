import { DashBoardComponent } from './pages/dashboard/dashboard.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';

import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { VerificationCodeComponent } from './pages/verification-code/verification-code.component';

export const routes = [
  { path: "", component: LoginComponent }, 
  { path: "dashboard", component: DashBoardComponent }, 
  { path: "search-page", component: SearchPageComponent }, 
  {path: "register", component: RegisterComponent},
  {path: "verification-code", component: VerificationCodeComponent},
  {path: "search-page", component: SearchPageComponent}
];

export const navigatableComponents = [
  LoginComponent,
  RegisterComponent,
  VerificationCodeComponent,
  SearchPageComponent,
  DashBoardComponent
];