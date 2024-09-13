import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { OtpPageComponent } from './otp-page/otp-page.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ConfirmDeliveryCodeComponent } from './confirm-delivery-code/confirm-delivery-code.component';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: 'login',
        component: SignInComponent,
        data: {
          animationState: 'isLeft',
        },
      },
      {
        path: 'reset',
        component: ForgotPasswordComponent,
        data: {
          animationState: 'isRight',
        },
      },
      {
        path: 'otp',
        component: OtpPageComponent,
        data: {
          animationState: 'isLeft',
        },
      },
      {
        path: 'password',
        component: ChangePasswordComponent,
        data: {
          animationState: 'isRight',
        },
      },
      {
        path: 'confirm-code/:id', //MjU1NzQyMDM2NjA5
        component: ConfirmDeliveryCodeComponent,
      },
      /*{
        path: 'reset',
        loadComponent: () =>
          import('./forgot-password/forgot-password.component').then(
            (m) => m.ForgotPasswordComponent
          ),
        data: {
          animationState: 'isRight',
        },
      },
      {
        path: 'otp',
        loadComponent: () =>
          import('./otp-page/otp-page.component').then(
            (o) => o.OtpPageComponent
          ),
        data: {
          animationState: 'isLeft',
        },
      },
      {
        path: 'password',
        loadComponent: () =>
          import('./change-password/change-password.component').then(
            (c) => c.ChangePasswordComponent
          ),
        data: {
          animationState: 'isRight',
        },
      },
      {
        path: 'confirm-code/:id', //MjU1NzQyMDM2NjA5
        loadComponent: () =>
          import(
            './confirm-delivery-code/confirm-delivery-code.component'
          ).then((c) => c.ConfirmDeliveryCodeComponent),
      },*/
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
