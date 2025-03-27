import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RouterModule } from '@angular/router';

import {AuthRoutingModule} from './auth-routing.module';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {TwoFactorComponent} from './two-factor/two-factor.component';
import {LogoutComponent} from './logout/logout.component';
import {AuthLayoutComponent} from './auth-layout/auth-layout.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';


import {MatButton, MatButtonModule} from '@angular/material/button';
import {MatSlideToggle, MatSlideToggleModule} from '@angular/material/slide-toggle';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import {SharedModule} from '../shared/shared.module';
import { MyProfileComponent } from './my-profile/my-profile.component';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    LogoutComponent,
    TwoFactorComponent,
    AuthLayoutComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    MyProfileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    AuthRoutingModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatButton,
    MatSlideToggle,
    ReactiveFormsModule,
    SharedModule,
  ],
  providers: []
})
export class AuthModule { }
