import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportClassComponent } from './report-class.component';

describe('ReportClassComponent', () => {
  let component: ReportClassComponent;
  let fixture: ComponentFixture<ReportClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportClassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
