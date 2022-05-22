import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceManagementService {

  readonly apiUrl = "https://localhost:7287"

  constructor(private http: HttpClient) { }

  // Attendee
  getAllAttendee():Observable<any[]>{
    return this.http.get<any>(this.apiUrl + '/get-all-attendee');
  }

}
