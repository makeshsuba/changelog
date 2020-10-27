import { Routes } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { LogListComponent } from '../log-list/log-list.component';


export const appRoute: Routes =
  [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    {
      path: 'list',
      component: LogListComponent
    }
  ];
