import { TestBed } from '@angular/core/testing';

import { AttendanceManagementService } from './attendance-management.service';

describe('AttendanceManagementService', () => {
  let service: AttendanceManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttendanceManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
