import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import anime from 'animejs';

export type PopupState = 'success' | 'error' | 'warning' | 'info';

@Component({
  selector: 'app-popup-message-dialog',
  imports: [MatIconModule, MatButtonModule, MatDialogModule, TranslocoModule],
  templateUrl: './popup-message-dialog.component.html',
  styleUrl: './popup-message-dialog.component.scss',
  standalone: true,
})
export class PopupMessageDialogComponent implements AfterViewInit {
  @Output() buttonClicked = new EventEmitter<void>();
  constructor(
    private _ref: MatDialogRef<PopupMessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      state: PopupState;
      message: string;
    }
  ) {}
  closePopup(event: MouseEvent) {
    this.buttonClicked.emit();
    anime({
      targets: '.popup-message-panel',
      opacity: [1, 0],
      scale: [1, 0.5],
      duration: 200,
      easing: 'easeInExpo',
      complete: () => this._ref.close(),
    });
    //this._ref.close();
  }
  ngAfterViewInit(): void {
    anime({
      targets: '.popup-message-panel',
      opacity: [0, 1],
      scale: [0.5, 1],
      duration: 500,
      easing: 'easeOutExpo',
    });
  }
}
