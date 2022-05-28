import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-add-edit-attendee',
  templateUrl: './add-edit-attendee.component.html',
  styleUrls: ['./add-edit-attendee.component.css']
})
export class AddEditAttendeeComponent implements OnInit {

  attendeeList$!: Observable<any[]>
  role$: any = ["Student", "Teacher"]

  constructor(private service: AttendanceManagementService) { }

  @Input() attendee:any
  id: number = 0
  name: string = ""
  email: string = ""
  role: string = ""

  ngOnInit(): void {
    this.id = this.attendee.id
    this.name = this.attendee.name
    this.email = this.attendee.email
    this.role = this.attendee.role
  }

  addAttendee() {
    var attendee = {
      name:this.name,
      email:this.email,
      role:this.role
    }
    this.service.addAttendee(attendee).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-attendee-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showAddSuccess = document.getElementById('add-attendee-success-alert');
      if(showAddSuccess) {
        showAddSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showAddSuccess) {
          showAddSuccess.style.display = "none"
        }
      }, 4000);
    })
  }

  updateAttendee() {
    var attendee = {
      id:this.id,
      name:this.name,
      email:this.email,
      role:this.role
    }
    var id:number = this.id;
    this.service.updateAttendee(id,attendee).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-attendee-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showUpdateSuccess = document.getElementById('update-attendee-success-alert');
      if(showUpdateSuccess) {
        showUpdateSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showUpdateSuccess) {
          showUpdateSuccess.style.display = "none"
        }
      }, 4000);
    })

  }
}
