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
  isLogin: boolean = false
  token: string = ""
  user: any

  constructor(private authService: AuthService, private router: Router){
    var checkExist = localStorage.getItem("token")

    if(checkExist != null){
      var token = localStorage.getItem("token")

      this.authService.getUser(token).subscribe((res:any) => {
        this.user = res

        if(this.user != null){
          this.isLogin = true
        }

        if(!this.isLogin)
          this.router.navigate(['/login'])
      })
    }
  }

  logout(){
    localStorage.clear()
    window.location.reload()
  }

}
