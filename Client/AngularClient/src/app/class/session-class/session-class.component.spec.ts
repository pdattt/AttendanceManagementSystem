import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionClassComponent } from './session-class.component';

describe('SessionClassComponent', () => {
  let component: SessionClassComponent;
  let fixture: ComponentFixture<SessionClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SessionClassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SessionClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
