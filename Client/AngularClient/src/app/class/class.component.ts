import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.css']
})
export class ClassComponent implements OnInit {

  user: any
  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit(): void {
    var checkExist = sessionStorage.getItem("token")

    if(checkExist != null){
      var token = sessionStorage.getItem("token")

      this.authService.getUser(token).subscribe((res:any) => {
        this.user = res

        if(this.user == null){
          this.route.navigate(['/login'])
        }
      })
    }
    else
      this.route.navigate(['/login'])
  }
}
