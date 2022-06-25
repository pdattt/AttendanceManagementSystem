import { Component, Input, OnInit } from '@angular/core';
import { AttendanceManagementService } from 'src/app/attendance-management.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-report-event',
  templateUrl: './report-event.component.html',
  styleUrls: ['./report-event.component.css']
})
export class ReportEventComponent implements OnInit {

  @Input() semesterId: any
  @Input() eventID: any
  checkins: any = []
  unassignedCheckins: any = []

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.checkins = this.service.countCheckIn(this.semesterId, "event", this.eventID)
    this.unassignedCheckins = this.service.countUnassignedCheckInsInEvent(this.semesterId, this.eventID)
  }

  exportExcel(){
    let fileName= "report_event_" + this.semesterId + this.eventID + "_" + '.xlsx';
    
    let element = document.getElementById('checkin_event_' + this.eventID);
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
 
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Checkin_Assingned_Attendees');
 
    /* save to file */  
    XLSX.writeFile(wb, fileName);
  }

}
