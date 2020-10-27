import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, } from '@angular/material/button';
import { MatIconModule, } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule } from '@angular/router';
import { appRoute } from './routing/route';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { MatChipsModule } from '@angular/material/chips';
import { ToastrModule } from 'ngx-toastr';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LoginComponent } from './login/login.component';
import { UserRegisterComponent } from './user-register/user-register.component';
import { LogListComponent } from './log-list/log-list.component';
import { ModalContentComponent } from './modal-content/modal-content.component';
import { CommonModule } from '@angular/common';
import { TokenInterceptor } from './interceptors/http-token.interceptors';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoaderService } from './services/load-spinner.service';

const fbLoginOptions = {
  scope: 'email',
  return_scopes: true,
  enable_profile_selector: true
};

const googleLoginOptions = {
  scope: 'profile email'
};

@NgModule({
  declarations: [
    AppComponent, LoginComponent, UserRegisterComponent, LogListComponent,
    ModalContentComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    SocialLoginModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    NgbModule,

    ToastrModule.forRoot(),
    RouterModule.forRoot(appRoute),
  ],
  providers: [{
    provide: 'SocialAuthServiceConfig',
    useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider(
            '477164274231-untlfno1n8gok02vg7d0h6n8d9d6idt2.apps.googleusercontent.com', googleLoginOptions
          ),
        },
        {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider('627535377943278'), fbLoginOptions
        }
      ],
    } as SocialAuthServiceConfig,
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }, NgbActiveModal, LoaderService
  ],
  bootstrap: [AppComponent],
  entryComponents: [ModalContentComponent]
})
export class AppModule { }
