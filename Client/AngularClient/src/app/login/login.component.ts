import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  isLogin: boolean = false;

  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit(): void {
    var checkExist = sessionStorage.getItem("token")

    if(checkExist != null){
      var token = sessionStorage.getItem("token")

      this.authService.getUser(token).subscribe((res:any) => {
        this.user = res

        if(this.user != null){
          this.route.navigate(['/attendee'])
        }
      })
    }
  }

  async login(){

    if(this.username.length == 0 || this.password.length == 0){
      var showLoginFailed = document.getElementById('login-empty-alert');
        if(showLoginFailed) {
          showLoginFailed.style.display = "block";
        }
        setTimeout(function() {
          if(showLoginFailed) {
            showLoginFailed.style.display = "none"
          }
        }, 4000);
      return
    }

    var user = {
      username: this.username,
      password: this.password
    }

    this.authService.login(user).subscribe(async (res:any) =>{
      this.token = res

      await this.authService.getUser(this.token).subscribe(data => {
        if(data != null){
          sessionStorage.setItem("token", this.token)

          var showLoginSuccess = document.getElementById('login-success-alert');
          if(showLoginSuccess) {
            showLoginSuccess.style.display = "block";
          }
          setTimeout(function() {
            if(showLoginSuccess) {
              showLoginSuccess.style.display = "none"
            }
          }, 4000)

          window.location.reload()

        }else{
          var showLoginFailed = document.getElementById('login-failed-alert');
          if(showLoginFailed) {
            showLoginFailed.style.display = "block";
          }
          setTimeout(function() {
            if(showLoginFailed) {
              showLoginFailed.style.display = "none"
            }
          }, 4000)
        }
      })
    })
  }
}
