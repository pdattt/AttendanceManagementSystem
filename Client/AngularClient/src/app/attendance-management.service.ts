import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceManagementService {

  //private readonly apiUrl = "http://ams.somee.com/api/"
  private readonly apiUrl = "https://localhost:7287/api/"
  private readonly TOKEN_NAME = 'token';
  header = new HttpHeaders()

  constructor(private http: HttpClient) { 
    let token = sessionStorage.getItem(this.TOKEN_NAME)

    if(token != null){
      this.header = this.header.set("Authorization", "Bearer " + token) 
    }
  }

  // Attendee --------------------------------------------------------------------------
  getAllAttendees():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'attendee/get-all-attendees', {headers: this.header});
  }

  addAttendee(data:any) {
    return this.http.post(this.apiUrl + 'attendee/add-new-attendee', data, {headers: this.header});
  }

  updateAttendee(id:number, data:any) {
    return this.http.put(this.apiUrl + `attendee/update-attendee-by-id/?id=${id}`, data, {headers: this.header});
  }

  deleteAttendee(id:number) {
    return this.http.delete(this.apiUrl + `attendee/delete-attendee-by-id/?id=${id}`, {headers: this.header})
  }


  // Event ------------------------------------------------------------------------------
  getAllEvents():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'event/get-all-events', {headers: this.header});
  }

  getEventById(id:number):any{
    return this.http.get<any>(this.apiUrl + `event/get-event-by-id?id=${id}`, {headers: this.header})
  }

  getAttendeeInEvent(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `event/get-attendees-in-event?id=${id}`, {headers: this.header})
  }

  getAvailableAttendeeInEvent(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `event/get-available-attendees-in-event?id=${id}`, {headers: this.header});
  }

  addEvent(data:any) {
    return this.http.post(this.apiUrl + 'event/add-new-event', data, {headers: this.header});
  }

  addAttendeeToEvent(id:number, data: any[]){
    return this.http.post(this.apiUrl + `event/add-attendees-to-event?eventId=${id}`, data, {headers: this.header})
  }

  addAllAttendeeToEvent(id:number){
    return this.http.post(this.apiUrl + `event/add-all-attendees-to-event?eventId=${id}`, {headers: this.header})
  }

  updateEvent(id:number, data:any) {
    return this.http.put(this.apiUrl + `event/update-event-by-id/?id=${id}`, data, {headers: this.header});
  }

  deleteEvent(id:number) {
    return this.http.delete(this.apiUrl + `event/delete-event-by-id/?id=${id}`, {headers: this.header})
  }

  removeAttendeeFromEvent(eventId:number, attendeeId: any){
    return this.http.delete(this.apiUrl + `event/remove-attendee-from-event?eventId=${eventId}&attendeeId=${attendeeId}`, {headers: this.header})
  }

  // Class ------------------------------------------------------------------------------
  getAllClasses():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + 'class/get-all-classes', {headers: this.header});
  }

  getClassById(id:number):any{
    return this.http.get<any>(this.apiUrl + `class/get-class-by-id?id=${id}`, {headers: this.header})
  }

  getAttendeeInClass(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `class/get-attendees-in-class?id=${id}`, {headers: this.header});
  }

  getAvailableAttendeeInClass(id: number):Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `class/get-available-attendees-in-class?id=${id}`, {headers: this.header});
  }

  getDaysOfWeek(id: number):any{
    return this.http.get(this.apiUrl + `class/get-days-of-week?id=${id}`, {headers: this.header})
  }

  addClass(data:any) {
    return this.http.post(this.apiUrl + 'class/add-new-class', data, {headers: this.header});
  }

  addAttendeeToClass(id:number, data: any[]){
    return this.http.post(this.apiUrl + `class/add-attendees-to-class?classId=${id}`, data, {headers: this.header})
  }

  addAllAttendeeToClass(id:number){
    return this.http.post(this.apiUrl + `class/add-all-attendees-to-class?classId=${id}`, {headers: this.header})
  }

  updateClass(id:number, data:any) {
    return this.http.put(this.apiUrl + `class/update-class-by-id/?id=${id}`, data, {headers: this.header});
  }

  deleteClass(id:number) {
    return this.http.delete(this.apiUrl + `class/delete-class-by-id?id=${id}`, {headers: this.header})
  }

  removeAttendeeFromClass(classId:number, attendeeId: any){
    return this.http.delete(this.apiUrl + `class/remove-attendee-from-class?classId=${classId}&attendeeId=${attendeeId}`, {headers: this.header})
  }
  
  // Session
  getAllSemesterIds(): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + "session/get-all-semester-ids", {headers: this.header})
  }

  getAllSession(semesterId: string, type: string, cls_eve_id: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-all-attendance-sessions?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}`, {headers: this.header})
  }

  getAllCheckin(semesterId: string, type: string, cls_eve_id: string, date: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-all-check-ins?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}&date=${date}`, {headers: this.header})
  }

  getCheckInByCardId(semesterId: string, type: string, cls_eve_id: string, date: string, cardId: string): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + `session/get-check-in-by-card-id?semesterId=${semesterId}&type=${type}&cls_eve_id=${cls_eve_id}&date=${date}&cardId=${cardId}`, {headers: this.header})
  }

  generateClassSession(id: number){
    return this.http.get(this.apiUrl + `session/generate-class-session?id=${id}`, {headers: this.header})
  }

  generateEventSession(id: number){
    return this.http.get(this.apiUrl + `session/generate-event-session?id=${id}`, {headers: this.header})
  }
}
