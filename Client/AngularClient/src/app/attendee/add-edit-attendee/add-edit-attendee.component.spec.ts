import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditAttendeeComponent } from './add-edit-attendee.component';

describe('AddEditAttendeeComponent', () => {
  let component: AddEditAttendeeComponent;
  let fixture: ComponentFixture<AddEditAttendeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditAttendeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditAttendeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
