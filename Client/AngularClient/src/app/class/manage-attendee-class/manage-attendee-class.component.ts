import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-manage-attendee-class',
  templateUrl: './manage-attendee-class.component.html',
  styleUrls: ['./manage-attendee-class.component.css']
})
export class ManageAttendeeClassComponent implements OnInit {

  private routeSub!: Subscription;
  class!: any
  classID: number = 0
  className: string = ""
  classDaysOfWeek: string = ""
  location: string = ""
  classStartTime: string = ""
  classEndTime: string = ""
  classDateStart: string = ""
  classDateEnd: string = ""

  // Attendee variables
  attendeesInEvent!: Observable<any[]>
  attendeesNotInEvent!: Observable<any[]>
  availableAttendees!: Observable<any[]>
  activateAddAttendeeToEvent:boolean = false
  attendeeToJoin: any = []
  attendeeToRemove: any = []
  attendanceSessions!: Observable<any[]>

  // Sessions variables
  semesterId: string = ""
  semesterIds!: any[]
  sessionDate: string = ""

  constructor(private route:ActivatedRoute, private service: AttendanceManagementService) { }

  ngOnInit(): void {    
    this.routeSub = this.route.params.subscribe(params => {
      this.classID = params['id']
      this.service.getClassById(this.classID).subscribe((res:any) => {
        this.class = res

        this.className = this.class.className
        this.classDaysOfWeek = this.class.daysOfWeek
        this.location = this.class.location
        this.classStartTime = this.class.classStartTime
        this.classEndTime = this.class.classEndTime
        this.classDateStart = this.class.classDateStart
        this.classDateEnd = this.class.classDateEnd
        }
      )
    })
    
    this.attendeesInEvent = this.service.getAttendeeInClass(this.classID)

    this.service.getAllSemesterIds().subscribe(res => {
      this.semesterIds = res
      this.semesterId = this.semesterIds[0]
      this.attendanceSessions = this.service.getAllSession(this.semesterId, "class", this.classID.toString())
    })
  }

  toogleAddAttendee(){
    if(this.activateAddAttendeeToEvent)
      this.activateAddAttendeeToEvent = false
    else{
      this.activateAddAttendeeToEvent = true
      this.availableAttendees = this.service.getAvailableAttendeeInClass(this.classID)
    }
  }

  addAllAttendeeToEvent(){
    this.availableAttendees.forEach(attendee => {
      this.attendeeToJoin.push(attendee)
    });

    this.attendeesInEvent = this.service.getAttendeeInClass(this.classID)
    this.availableAttendees = this.service.getAvailableAttendeeInClass(this.classID)
  }

  importExcelFile(){

  }

  addAttendeeToEvent(att: any){
    this.attendeeToJoin.push(att.id)

    this.service.addAttendeeToEvent(this.classID, this.attendeeToJoin).subscribe(res =>{
      this.attendeesInEvent = this.service.getAttendeeInClass(this.classID)
      this.availableAttendees = this.service.getAvailableAttendeeInClass(this.classID)
    })
  }

  removeAttendee(att: any){
    if(confirm(`Are you sure you want to remove this attendee from class with code ${att.id}`)) {
      this.service.removeAttendeeFromEvent(this.classID, att.id).subscribe(res =>{
        this.attendeesInEvent = this.service.getAttendeeInClass(this.classID)
        this.availableAttendees = this.service.getAvailableAttendeeInClass(this.classID)
      })
    }
  }

  getCheckin(sessionDate: string, cardId: string){
      this.service.getCheckInByCardId(this.semesterId, "class", this.classID.toString(), sessionDate, cardId).subscribe(res => {
        return res
    });
  }

  selectSemester(id: string){
    this.semesterId = id
    this.attendanceSessions = this.service.getAllSession(this.semesterId, "class", this.classID.toString())
  }
}

