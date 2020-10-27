
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';



@Injectable({ providedIn: 'root' })

export class AuthService {
  userName: string;
  toastrMessage: string;
  constructor(private httpClient: HttpClient) { }
  apiUrl = environment.apiUrl;

  login(model: any) {
    return this.httpClient.post(this.apiUrl + 'registration/login', model)
      .pipe(
        map((res: any) => {
          const user = res;
          if (user) {
            this.userName = user.username;
            localStorage.setItem('token', user.token);
          }
        }));
  }
  register(model: any) {
    return this.httpClient.post(this.apiUrl + 'registration/register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }
}
