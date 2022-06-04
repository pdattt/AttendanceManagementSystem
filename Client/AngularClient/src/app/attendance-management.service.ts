import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceManagementService {

  private readonly apiUrl = "https://localhost:7287/api/"

  constructor(private http: HttpClient) { }

  // Attendee --------------------------------------------------------------------------
  getAllAttendees():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'attendee/get-all-attendees');
  }

  addAttendee(data:any) {
    return this.http.post(this.apiUrl + 'attendee/add-new-attendee', data);
  }

  updateAttendee(id:number, data:any) {
    return this.http.put(this.apiUrl + `attendee/update-attendee-by-id/?id=${id}`, data);
  }

  deleteAttendee(id:number) {
    return this.http.delete(this.apiUrl + `attendee/delete-attendee-by-id/?id=${id}`)
  }


  // Event ------------------------------------------------------------------------------
  getAllEvents():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'event/get-all-events');
  }

  getEventById(id:number):any{
    return this.http.get<any>(this.apiUrl + `event/get-event-by-id?id=${id}`)
  }

  getAttendeeInEvent(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `event/get-attendees-in-event?id=${id}`);
  }

  getAvailableAttendeeInEvent(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `event/get-available-attendees-in-event?id=${id}`);
  }

  addEvent(data:any) {
    return this.http.post(this.apiUrl + 'event/add-new-event', data);
  }

  addAttendeeToEvent(id:number, data: any[]){
    return this.http.post(this.apiUrl + `event/add-attendees-to-event?eventId=${id}`, data)
  }

  updateEvent(id:number, data:any) {
    return this.http.put(this.apiUrl + `event/update-event-by-id/?id=${id}`, data);
  }

  deleteEvent(id:number) {
    return this.http.delete(this.apiUrl + `event/delete-event-by-id/?id=${id}`)
  }

  removeAttendeeFromEvent(eventId:number, attendeeId: any){
    return this.http.delete(this.apiUrl + `event/remove-attendee-from-event?eventId=${eventId}&attendeeId=${attendeeId}`)
  }

  // Class ------------------------------------------------------------------------------
  getAllClasses():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'class/get-all-classes');
  }

  getClassById(id:number):any{
    return this.http.get<any>(this.apiUrl + `class/get-class-by-id?id=${id}`)
  }

  getAttendeeInClass(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `class/get-attendees-in-class?id=${id}`);
  }

  getAvailableAttendeeInClass(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `class/get-available-attendees-in-class?id=${id}`);
  }

  getDaysOfWeek(id: number):any{
    return this.http.get(this.apiUrl + `class/get-days-of-week?id=${id}`)
  }

  addClass(data:any) {
    return this.http.post(this.apiUrl + 'class/add-new-class', data);
  }

  addAttendeeToClass(id:number, data: any[]){
    return this.http.post(this.apiUrl + `class/add-attendees-to-class?classId=${id}`, data)
  }

  updateClass(id:number, data:any) {
    return this.http.put(this.apiUrl + `class/update-class-by-id/?id=${id}`, data);
  }

  deleteClass(id:number) {
    return this.http.delete(this.apiUrl + `class/delete-class-by-id?id=${id}`)
  }

  removeAttendeeFromClass(classId:number, attendeeId: any){
    return this.http.delete(this.apiUrl + `class/remove-attendee-from-class?classId=${classId}&attendeeId=${attendeeId}`)
  }
  
  // Session
  getAllSemesterIds(): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + "session/get-all-semester-ids")
  }

  getAllSession(semesterId: string, type: string, cls_eve_id: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-all-attendance-sessions?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}`)
  }

  getAllCheckin(semesterId: string, type: string, cls_eve_id: string, date: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-all-check-ins?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}&date=${date}`)
  }

  getCheckInByCardId(semesterId: string, type: string, cls_eve_id: string, date: string, cardId: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-check-in-by-card-id?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}&date=${date}&cardId=${cardId}`)
  }
}
