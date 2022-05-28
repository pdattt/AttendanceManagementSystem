import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAttendeeEventComponent } from './manage-attendee-event.component';

describe('ManageAttendeeEventComponent', () => {
  let component: ManageAttendeeEventComponent;
  let fixture: ComponentFixture<ManageAttendeeEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAttendeeEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAttendeeEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
