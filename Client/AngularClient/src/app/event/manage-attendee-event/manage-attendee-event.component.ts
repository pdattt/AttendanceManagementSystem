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

  // Attendee variables
  attendeesInEvent!: Observable<any[]>
  attendeesNotInEvent!: Observable<any[]>
  availableAttendees!: Observable<any[]>
  activateAddAttendeeToEvent:boolean = false
  attendeeToJoin: any = []
  attendeeToRemove: any = []

  // Sessions variables
  attendanceSessions!: Observable<any[]>
  checkins!: Observable<any[]>

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

    this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
    this.attendanceSessions = this.service.getAllSession("20221", "event", this.eventID.toString())

    // this.checkins = this.service.getCheckInByCardId("20221", "event", this.eventID.toString(), )
  }

  toogleAddAttendee(){
    if(this.activateAddAttendeeToEvent)
      this.activateAddAttendeeToEvent = false
    else{
      this.activateAddAttendeeToEvent = true
      this.availableAttendees = this.service.getAvailableAttendee(this.eventID)
    }
  }

  addAllAttendeeToEvent(){

  }

  importExcelFile(){

  }

  addAttendeeToEvent(att: any){
    this.attendeeToJoin.push(att.id)

    this.service.addAttendeeToEvent(this.eventID, this.attendeeToJoin).subscribe(res =>{
      this.attendeeToJoin = []
      this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
      this.availableAttendees = this.service.getAvailableAttendee(this.eventID)
    })
  }

  removeAttendee(att: any){
    if(confirm(`Are you sure you want to remove this attendee from event with code ${att.id}`)) {
      this.service.removeAttendeeFromEvent(this.eventID, att.id).subscribe(res =>{
        this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
        this.availableAttendees = this.service.getAvailableAttendee(this.eventID)
      })
    }
  }

  getCheckin(sessionDate: string, cardId: string){
      this.service.getCheckInByCardId("20221", "event", this.eventID.toString(), sessionDate, cardId).subscribe(res => {
        return res
    });
    
  }
}
