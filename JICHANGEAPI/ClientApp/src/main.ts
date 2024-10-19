import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import {
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { AppRoutingModule, routes } from './app/app-routing.module';
import { provideAnimations } from '@angular/platform-browser/animations';
import {
  authInterceptor,
  timeoutInterceptor,
} from './app/core/interceptors/client.interceptor';
import { NgIdleKeepaliveModule } from '@ng-idle/keepalive';
import { AppConfigService } from './app/core/services/app-config.service';
import { MatDialogModule } from '@angular/material/dialog';
import {
  provideRouter,
  withHashLocation,
  withViewTransitions,
} from '@angular/router';
import { toast, NgxSonnerToaster } from 'ngx-sonner';
import { provideNativeDateAdapter } from '@angular/material/core';
import { provideTransloco } from '@ngneat/transloco';
import { TranslocoHttpLoader } from './app/transloco-loader';
import { environment } from './environments/environment';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom([
      AppRoutingModule,
      HttpClientModule,
      //TranslocoRootModule,
      MatDialogModule,
      NgIdleKeepaliveModule.forRoot(),
    ]),
    provideAnimations(),
    provideHttpClient(withInterceptors([authInterceptor, timeoutInterceptor])),
    provideRouter(routes, withHashLocation(), withViewTransitions()),
    provideNativeDateAdapter(),
    provideTransloco({
      config: {
        //availableLangs: ['en', 'sw', 'ln', 'fr'],
        availableLangs: ['en'],
        defaultLang: localStorage.getItem('activeLang')
          ? localStorage.getItem('activeLang')?.toString()
          : 'en',
        // Remove this option if your application doesn't support changing language in runtime.
        reRenderOnLangChange: true,
        prodMode: environment.production,
        //prodMode: !isDevMode(),
      },
      loader: TranslocoHttpLoader,
    }),
    //provideToastr(),
    // provideToastr({
    //   timeOut: 5000,
    //   positionClass: 'toast-top-right',
    //   preventDuplicates: true,
    // }),
    AppConfigService,
  ],
}).catch((err) => console.log(err));
