import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { SocialAuthService, FacebookLoginProvider, GoogleLoginProvider, SocialUser } from 'angularx-social-login';
import { AuthService } from '../services/auth-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  title = 'changelogUI';
  loginForm: FormGroup;
  user: SocialUser;
  loggedIn = false;
  errorMessage: string;
  registerShow = false;
  returnUrl: string;
  model: any;
  submitted = false;

  constructor(
    private socialAuthService: SocialAuthService, private authService: AuthService,
    private router: Router, private toastrService: ToastrService,
    private formBuilder: FormBuilder) { }

  socialSignIn(socialProvider: string): void {
    let providerId: string;
    if (socialProvider === 'google') {
      providerId = GoogleLoginProvider.PROVIDER_ID;
    }
    else if (socialProvider === 'facebook') {
      providerId = FacebookLoginProvider.PROVIDER_ID;
    }

    this.socialAuthService.signIn(providerId).then(user => {
      localStorage.setItem('socialusers', JSON.stringify(user));
      const model = {
        username: JSON.parse(localStorage.getItem('socialusers')).email,
        password: JSON.parse(localStorage.getItem('socialusers')).id,
        provider: JSON.parse(localStorage.getItem('socialusers')).provider
      };
      this.authService.login(model).subscribe(next => { this.router.navigate(['/list']); },
        error => { console.log(error); });
    });
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.authService.login(this.loginForm.value).subscribe(next => { this.router.navigate(['/list']); },
      error => { console.log(error); });
  }

  signOut(): void {
    this.socialAuthService.signOut();
  }

  ngOnInit() {
    if (this.authService.toastrMessage) {
      this.toastrService.success(this.authService.toastrMessage);
      this.authService.toastrMessage = '';
    }
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  registerToggle() {
    this.registerShow = true;
  }

  getMessage(message: boolean) {
    this.registerShow = message;
  }
}
