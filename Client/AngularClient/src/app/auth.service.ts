import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl = "https://localhost:7287/api/user"

  constructor(private http: HttpClient) { }

  login(data: any){
    return this.http.post(this.apiUrl + '/login', data)
  }

  getUser(token: any){
    return this.http.get(this.apiUrl + `/get-user?token=${token}`)
  }
}
