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
  activateAddEditAttendeeComponent:boolean = false;

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.attendees = this.service.getAllAttendees()
  }

  modalAdd(){
    this.attendee = {
      id: 0,
      name:null,
      email:null,
      role:null
    }
    this.modalTitle = "Add Attendee";
    this.activateAddEditAttendeeComponent = true;
  }

  modalEdit(att: any){
    this.attendee = att;
    this.modalTitle = "Edit Attendee";
    this.activateAddEditAttendeeComponent = true;
  }

  delete(att: any){
    if(confirm(`Are you sure you want to delete this attendee with code ${att.id}`)) {
      this.service.deleteAttendee(att.id).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-attendee-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showDeleteSuccess = document.getElementById('delete-attendee-success-alert');
      if(showDeleteSuccess) {
        showDeleteSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showDeleteSuccess) {
          showDeleteSuccess.style.display = "none"
        }
      }, 4000);
      this.attendees = this.service.getAllAttendees();
      })
    }
  }

  modalClose(){
    this.activateAddEditAttendeeComponent = false;
    this.attendees = this.service.getAllAttendees()
  }

}
