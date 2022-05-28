import { Component, Input, OnInit } from '@angular/core';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-add-edit-event',
  templateUrl: './add-edit-event.component.html',
  styleUrls: ['./add-edit-event.component.css']
})
export class AddEditEventComponent implements OnInit {

  @Input() event:any
  eventID: number = 0
  eventName: string = ""
  eventDate: Date = new Date()
  location: string = ""
  eventStartTime: string = ""
  eventEndTime: string = ""

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.eventID = this.event.eventID
    this.eventName = this.event.eventName
    this.eventDate = this.event.eventDate
    this.location = this.event.location
    this.eventStartTime = this.event.eventStartTime
    this.eventEndTime = this.event.eventEndTime
  }



  addEvent() {
    var event = {
      eventName:this.eventName,
      eventDate:this.eventDate,
      location:this.location,
      eventStartTime:this.eventStartTime,
      eventEndTime:this.eventEndTime
    }
    this.service.addEvent(event).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-event-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showAddSuccess = document.getElementById('add-event-success-alert');
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

  updateEvent() {
    var event = {
      eventID:this.eventID,
      eventName:this.eventName,
      eventDate:this.eventDate,
      location:this.location,
      eventStartTime:this.eventStartTime,
      eventEndTime:this.eventEndTime
    }
    var id:number = this.eventID;
    this.service.updateEvent(id,event).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-event-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showUpdateSuccess = document.getElementById('update-event-success-alert');
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
