import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from '../attendance-management.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user:any
  username: string = ""
  password: string = ""

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
  }

  login(){
    var user = {
      username: this.username,
      password: this.password
    }

    this.service.login(user).subscribe(res =>{
      this.user = res
      console.log(this.user)
    })
  }
}
