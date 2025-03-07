import { Injectable, OnInit } from '@angular/core';
//import { LoginResponse } from '../models/login-response';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ErrorMessageDialogComponent } from 'src/app/components/dialogs/error-message-dialog/error-message-dialog.component';
import {
  BankLoginResponse,
  VendorLoginResponse,
} from '../models/login-response';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { PopupMessageDialogComponent } from 'src/app/components/dialogs/popup-message-dialog/popup-message-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class AppConfigService {
  public userProfile!: VendorLoginResponse | BankLoginResponse;
  constructor(
    private dialog: MatDialog,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {
    this.parseUserProfile();
  }
  /**
   * Registers icons to be available to <mat-icon>
   * @param icons - List of icon names with .svg file extension excluded
   * @param path - Folder path where to find icons specified
   */
  public addIcons(icons: string[], path: string) {
    icons.forEach((icon) => {
      this.iconRegistry.addSvgIcon(
        icon,
        this.sanitizer.bypassSecurityTrustResourceUrl(`${path}/${icon}.svg`)
      );
    });
  }
  private parseUserProfile() {
    let userProfile = sessionStorage.getItem('userProfile');
    if (userProfile) {
      this.userProfile = JSON.parse(userProfile) as
        | VendorLoginResponse
        | BankLoginResponse;
    }
  }
  getLoginResponse() {
    this.parseUserProfile();
    return this.userProfile;
  }
  openDisabledCloseMessageDialog(title: string, message: string) {
    let dialogRef = this.dialog.open(ErrorMessageDialogComponent, {
      width: '400px',
      backdropClass: 'custom-dialog-overlay',
      disableClose: true,
      data: {
        title: title,
        message: message,
      },
    });
    return dialogRef;
  }
  openStateDialog(state: 'success' | 'error', message: string) {
    return this.dialog.open(PopupMessageDialogComponent, {
      data: {
        state: state,
        message: message,
      },
    });
  }
  getJwtTokenFromSessionStorage() {
    this.parseUserProfile();
    return this.userProfile ? this.userProfile.Token : '';
  }
  getUserIdFromSessionStorage() {
    this.parseUserProfile();
    return this.userProfile ? this.userProfile.Usno : 0;
  }
  setItem(key: string, value: any) {
    sessionStorage.setItem(key, value);
  }
  clearSessionStorage() {
    sessionStorage.clear();
  }
}
