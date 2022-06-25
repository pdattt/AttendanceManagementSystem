import { Component, Input, OnInit } from '@angular/core';
import { AttendanceManagementService } from 'src/app/attendance-management.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-report-class',
  templateUrl: './report-class.component.html',
  styleUrls: ['./report-class.component.css']
})
export class ReportClassComponent implements OnInit {

  @Input() semesterId: any
  @Input() classID: any
  checkins: any = []

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.checkins = this.service.countCheckIn(this.semesterId, "class", this.classID)
  }

  exportExcel(){
    let fileName= "report_class_" + this.semesterId + this.classID + "_" + '.xlsx';
    
    let element = document.getElementById('checkin_class_' + this.classID);
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
 
    /* generate workbook and add the worksheet */
    var wb: XLSX.WorkBook = XLSX.utils.book_new();

    
    XLSX.utils.book_append_sheet(wb, ws, 'Checkin_Assingned_Attendees');
 
    /* save to file */  
    XLSX.writeFile(wb, fileName);
  }
}
