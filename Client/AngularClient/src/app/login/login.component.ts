import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from '../attendance-management.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user:any
  token: string = ""
  username: string = ""
  password: string = ""

  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit(): void {
  }

  login(){
    var user = {
      username: this.username,
      password: this.password
    }

    this.authService.login(user).subscribe(res =>{
      this.token = res.toString()
      this.user = this.authService.getUser(this.token)
      
      if(this.user != null){
        localStorage.setItem("token", this.token)
        window.location.reload()
      }
      //this.route.navigate(['/attendee'])
    })
  }
}
