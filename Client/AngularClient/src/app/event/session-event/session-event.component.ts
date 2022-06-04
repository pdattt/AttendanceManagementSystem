import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-session-event',
  templateUrl: './session-event.component.html',
  styleUrls: ['./session-event.component.css']
})
export class SessionEventComponent implements OnInit {

  @Input() eventID: number = 0
  @Input() semesterId: string = ""
  @Input() sessionDate: string =""
  
  attendanceSessions!: Observable<any[]>
  checkins: any = []

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.attendanceSessions = this.service.getAllSession(this.semesterId, "event", this.eventID.toString())
    this.checkins = this.service.getAllCheckin(this.semesterId, "event", this.eventID.toString(), this.sessionDate)
  }
}
