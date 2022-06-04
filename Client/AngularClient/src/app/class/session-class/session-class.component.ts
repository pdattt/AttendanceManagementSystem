import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-session-class',
  templateUrl: './session-class.component.html',
  styleUrls: ['./session-class.component.css']
})
export class SessionClassComponent implements OnInit {

  @Input() classID: number = 0
  @Input() semesterId: string = ""
  @Input() sessionDate: string =""
  
  attendanceSessions!: Observable<any[]>
  checkins: any = []

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.attendanceSessions = this.service.getAllSession(this.semesterId, "class", this.classID.toString())
    this.checkins = this.service.getAllCheckin(this.semesterId, "class", this.classID.toString(), this.sessionDate)
  }
}
