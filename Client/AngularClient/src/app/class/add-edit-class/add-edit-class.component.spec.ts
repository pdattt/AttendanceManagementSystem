import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditClassComponent } from './add-edit-class.component';

describe('AddEditClassComponent', () => {
  let component: AddEditClassComponent;
  let fixture: ComponentFixture<AddEditClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditClassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
