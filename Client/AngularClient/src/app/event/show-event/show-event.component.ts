import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-show-event',
  templateUrl: './show-event.component.html',
  styleUrls: ['./show-event.component.css']
})
export class ShowEventComponent implements OnInit {

  events!: Observable<any[]>
  event: any
  modalTitle:string = ""
  activateAddEditEventComponent:boolean = false;

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.events = this.service.getAllEvents()
  }

  modalAdd(){
    this.event = {
      eventID: 0,
      eventName:null,
      eventDate:null,
      location:null,
      eventStartTime:null,
      eventEndTime:null
    }
    this.modalTitle = "Add Event";
    this.activateAddEditEventComponent = true;
  }

  modalEdit(eve: any){
    this.event = eve;
    this.modalTitle = "Edit Event";
    this.activateAddEditEventComponent = true;
  }

  delete(eve: any){
    if(confirm(`Are you sure you want to delete this event with code ${eve.eventID}`)) {
      this.service.deleteAttendee(eve.eventID).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-eve-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showDeleteSuccess = document.getElementById('delete-eve-success-alert');
      if(showDeleteSuccess) {
        showDeleteSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showDeleteSuccess) {
          showDeleteSuccess.style.display = "none"
        }
      }, 4000);
      this.events = this.service.getAllEvents();
      })
    }
  }

  modalClose(){
    this.activateAddEditEventComponent = false;
    this.events = this.service.getAllEvents()
  }
}
