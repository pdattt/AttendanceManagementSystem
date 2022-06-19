import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';
import * as XLSX from 'xlsx';

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

  exportExcel(date: string){
    let fileName= "report_class_" + this.classID + "_" + date + '.xlsx';
    
    let element = document.getElementById('checkin_class_' + this.classID + "_" + this.sessionDate);
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
 
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Checkin_Assigned_Attendees');
 
    /* save to file */  
    XLSX.writeFile(wb, fileName);
  }
}
