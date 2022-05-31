import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularClient';
  isAuthorize: boolean = false
  token: string = ""
  user: any

  constructor(private authService: AuthService, private router: Router){
    var checkExist = localStorage.getItem("token")

    if(checkExist != null){
      var token = localStorage.getItem("token")

      this.authService.getUser(token).subscribe((res:any) => this.user = res)
      console.log(token)
    }
  }
}
