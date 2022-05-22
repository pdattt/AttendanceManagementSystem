import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-show-attendee',
  templateUrl: './show-attendee.component.html',
  styleUrls: ['./show-attendee.component.css']
})
export class ShowAttendeeComponent implements OnInit {

  attendees!: Observable<any[]>
  attendee:any
  modalTitle:string = ""

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.attendees = this.service.getAllAttendee()
  }

  modalAdd(){

  }

  modalEdit(){

  }

  delete(){

  }

  modalClose(){

  }
}
