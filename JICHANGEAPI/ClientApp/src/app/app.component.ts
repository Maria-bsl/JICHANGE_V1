import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import {
  TRANSLOCO_SCOPE,
  TranslocoModule,
  TranslocoService,
} from '@ngneat/transloco';
import { CommonModule } from '@angular/common';
import { LoginService } from './core/services/login.service';
import { AppUtilities } from './utilities/app-utilities';
import { DisplayMessageBoxComponent } from './components/dialogs/display-message-box/display-message-box.component';
import { mainAnimations } from './components/layouts/main/router-transition-animations';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        RouterModule,
        TranslocoModule,
        CommonModule,
        DisplayMessageBoxComponent,
    ],
    providers: [{ provide: TRANSLOCO_SCOPE, useValue: { scope: 'auth' } }],
    animations: [mainAnimations]
})
export class AppComponent implements OnInit {
  constructor(private tr: TranslocoService, private snackbar: MatSnackBar) {}
  private onlineEventListener() {
    window.addEventListener('online', () => {
      let message = this.tr.translate('errors.connected');
      let action = this.tr.translate('actions.ok');
      this.snackbar.open(message, action);
    });
  }
  private offlineEventListener() {
    window.addEventListener('offline', () => {
      let message = this.tr.translate('errors.noInternet');
      let action = this.tr.translate('actions.ok');
      this.snackbar.open(message, action);
    });
  }
  private internetConnectionChangedListener() {
    this.onlineEventListener();
    this.offlineEventListener();
  }
  ngOnInit(): void {
    this.internetConnectionChangedListener();
  }
  prepareRoute(outlet: RouterOutlet, animate: string): boolean {
    return (
      outlet && outlet.activatedRouteData && outlet.activatedRouteData[animate]
    );
  }
}
