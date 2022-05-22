import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAttendeeComponent } from './show-attendee.component';

describe('ShowAttendeeComponent', () => {
  let component: ShowAttendeeComponent;
  let fixture: ComponentFixture<ShowAttendeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAttendeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAttendeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
