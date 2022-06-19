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
  attendanceSessions!: Observable<any[]>

  // Sessions variables
  semesterId: any = ""
  semesterIds!: any[]
  sessionDate: string = ""

  // Modal
  activateAddEditEventComponent:boolean = false
  activateEventReportModal: boolean = false

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

        this.service.getSemesterId(this.event.eventDate).subscribe(res => {
          this.semesterId = res
          this.attendanceSessions = this.service.getAllSession(this.semesterId, "event", this.eventID.toString())
          console.log(res)
          })
        }
      )
    })

    this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)

    // this.service.getAllSemesterIds().subscribe(res => {
    //   this.semesterIds = res
    //   this.semesterId = this.semesterIds[0]
    //   this.attendanceSessions = this.service.getAllSession(this.semesterId, "event", this.eventID.toString())
    // })
  }

  toogleAddAttendee(){
    if(this.activateAddAttendeeToEvent)
      this.activateAddAttendeeToEvent = false
    else{
      this.activateAddAttendeeToEvent = true
      this.availableAttendees = this.service.getAvailableAttendeeInEvent(this.eventID)
    }
  }

  addAllAttendeeToEvent(){
    this.service.addAllAttendeeToEvent(this.eventID).subscribe(res => {
      this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
      this.availableAttendees = this.service.getAvailableAttendeeInEvent(this.eventID)
    })
  }

  importExcelFile(){

  }

  addAttendeeToEvent(att: any){
    this.attendeeToJoin.push(att.id)

    this.service.addAttendeeToEvent(this.eventID, this.attendeeToJoin).subscribe(res =>{
      this.attendeeToJoin = []
      this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
      this.availableAttendees = this.service.getAvailableAttendeeInEvent(this.eventID)
    })
  }

  removeAttendee(att: any){
    if(confirm(`Are you sure you want to remove this attendee from event with code ${att.id}`)) {
      this.service.removeAttendeeFromEvent(this.eventID, att.id).subscribe(res =>{
        this.attendeesInEvent = this.service.getAttendeeInEvent(this.eventID)
        this.availableAttendees = this.service.getAvailableAttendeeInEvent(this.eventID)
      })
    }
  }

  getCheckin(sessionDate: string, cardId: string){
      this.service.getCheckInByCardId(this.semesterId, "event", this.eventID.toString(), sessionDate, cardId).subscribe(res => {
        return res
    });
  }

  selectSemester(id: string){
    this.semesterId = id
    this.attendanceSessions = this.service.getAllSession(this.semesterId, "event", this.eventID.toString())
  }

  generateSession(){

    this.service.generateEventSession(this.eventID).subscribe(res => {
      window.location.reload()
    })
  }

  modalClose(){
    this.activateAddEditEventComponent = false;
    this.service.getEventById(this.eventID).subscribe((res:any) => {
      this.event = res

      this.eventName = this.event.eventName
      this.eventDate = this.event.eventDate
      this.location = this.event.location
      this.eventStartTime = this.event.eventStartTime
      this.eventEndTime = this.event.eventEndTime
      }
    )
  }

  modalEdit(){
    var eve = {
      eventID:this.eventID,
      eventName:this.eventName,
      eventDate:this.eventDate,
      location:this.location,
      eventStartTime:this.eventStartTime,
      eventEndTime:this.eventEndTime
    }

    this.event = eve;
    this.activateAddEditEventComponent = true;
  }

  printReport(id: string){
    this.semesterId = id
    this.activateEventReportModal = true
  }
}
