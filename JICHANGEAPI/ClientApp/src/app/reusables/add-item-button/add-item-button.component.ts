import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
    selector: 'app-add-item-button',
    templateUrl: './add-item-button.component.html',
    styleUrls: ['./add-item-button.component.scss'],
    imports: [MatIconModule],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddItemButtonComponent {
  @Output() public buttonCliked = new EventEmitter<void>();
  @Input() public icon: string = 'add';
  @Input() public title: string = 'click me!';

  public onButtonClicked() {
    this.buttonCliked.emit();
  }
}
