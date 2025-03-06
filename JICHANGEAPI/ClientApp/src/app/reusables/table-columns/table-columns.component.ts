import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PerformanceUtils } from 'src/app/utilities/performance-utils';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@Component({
    selector: 'app-table-columns',
    imports: [CommonModule, ReactiveFormsModule, MatIconModule],
    templateUrl: './table-columns.component.html',
    styleUrls: ['./table-columns.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TableColumnsComponent {
  public PerformanceUtils: typeof PerformanceUtils = PerformanceUtils;
  @Input() public formGroup!: FormGroup;
  get headers(): FormArray {
    return this.formGroup.get('headers') as FormArray;
  }
}
