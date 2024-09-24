import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableColumnsComponent } from './table-columns.component';

describe('TableColumnsComponent', () => {
  let component: TableColumnsComponent;
  let fixture: ComponentFixture<TableColumnsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [TableColumnsComponent]
    });
    fixture = TestBed.createComponent(TableColumnsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
