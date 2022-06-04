import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAttendeeClassComponent } from './manage-attendee-class.component';

describe('ManageAttendeeClassComponent', () => {
  let component: ManageAttendeeClassComponent;
  let fixture: ComponentFixture<ManageAttendeeClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAttendeeClassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAttendeeClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
