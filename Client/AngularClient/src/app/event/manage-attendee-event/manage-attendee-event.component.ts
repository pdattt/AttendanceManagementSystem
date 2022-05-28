import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Observable, Subscription } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-manage-attendee-event',
  templateUrl: './manage-attendee-event.component.html',
  styleUrls: ['./manage-attendee-event.component.css']
})
export class ManageAttendeeEventComponent implements OnInit {

  private routeSub!: Subscription;
  event!: any
  eventID: number = 0
  eventName: string = ""
  eventDate: Date = new Date()
  location: string = ""
  eventStartTime: string = ""
  eventEndTime: string = ""
  attendees!: Observable<any[]>

  constructor(private route:ActivatedRoute, private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.eventID = params['id']
      this.service.getEventById(this.eventID).subscribe((res:any) => {
        this.event = res

        this.eventName = this.event.eventName
        this.eventDate = this.event.eventDate
        this.location = this.event.location
        this.eventStartTime = this.event.eventStartTime
        this.eventEndTime = this.event.eventEndTime
        }
      )
    })

    this.attendees = this.service.getAttendeeInEvent(this.eventID)
  }

  
}
