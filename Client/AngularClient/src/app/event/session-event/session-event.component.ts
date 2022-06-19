import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';
import * as XLSX from 'xlsx';

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

  exportExcel(date: string){
    let fileName= "report_event_" + this.eventID + "_" + date + '.xlsx';
    
    let element = document.getElementById('checkin_event_' + this.eventID + "_" + this.sessionDate);
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
 
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Checkin_Assingned_Attendees');
 
    /* save to file */  
    XLSX.writeFile(wb, fileName);
  }
}
