import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddItemButtonComponent } from './add-item-button.component';

describe('AddItemButtonComponent', () => {
  let component: AddItemButtonComponent;
  let fixture: ComponentFixture<AddItemButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddItemButtonComponent]
    });
    fixture = TestBed.createComponent(AddItemButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
