import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceManagementService {

  readonly apiUrl = "https://localhost:7287"

  constructor(private http: HttpClient) { }

  // Attendee --------------------------------------------------------------------------
  getAllAttendees():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + '/get-all-attendees');
  }

  addAttendee(data:any) {
    return this.http.post(this.apiUrl + '/add-new-attendee', data);
  }

  updateAttendee(id:number, data:any) {
    return this.http.put(this.apiUrl + `/update-attendee-by-id/?id=${id}`, data);
  }

  deleteAttendee(id:number) {
    return this.http.delete(this.apiUrl + `/delete-attendee-by-id/?id=${id}`)
  }


  // Event --------------------------------------------------------------------------
  getAllEvents():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + '/get-all-events');
  }

  getEventById(id:number):any{
    return this.http.get<any>(this.apiUrl + `/get-event-by-id?id=${id}`)
  }

  getAttendeeInEvent(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `/get-attendees-in-event?id=${id}`);
  }

  addEvent(data:any) {
    return this.http.post(this.apiUrl + '/add-new-event', data);
  }

  updateEvent(id:number, data:any) {
    return this.http.put(this.apiUrl + `/update-event-by-id/?id=${id}`, data);
  }

  deleteEvent(id:number) {
    return this.http.delete(this.apiUrl + `/delete-event-by-id/?id=${id}`)
  }

}
