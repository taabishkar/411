import { MyScheduleComponent } from './pages/my-schedule/my-schedule.component';
import { FriendsComponent } from './pages/friends/friends.component';
import { NotificationComponent } from './pages/notification/notification.component';
import { DashBoardComponent } from './pages/dashboard/dashboard.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';
import { ScheduleComponent } from './pages/schedule/schedule.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { VerificationCodeComponent } from './pages/verification-code/verification-code.component';

export const routes = [
  { path: "", component: LoginComponent }, 
  { path: "dashboard", component: DashBoardComponent }, 
  { path: "search-page", component: SearchPageComponent }, 
  {path: "register", component: RegisterComponent},
  {path: "verification-code", component: VerificationCodeComponent},
  {path: "search-page", component: SearchPageComponent},
  {path: "notification", component: NotificationComponent},
  {path: "friends", component: FriendsComponent},
  {path: "schedule", component: ScheduleComponent},
  {path: "my-schedule", component: MyScheduleComponent}
];

export const navigatableComponents = [
  LoginComponent,
  RegisterComponent,
  VerificationCodeComponent,
  SearchPageComponent,
  DashBoardComponent,
  NotificationComponent,
  FriendsComponent,
  ScheduleComponent,
  MyScheduleComponent
];