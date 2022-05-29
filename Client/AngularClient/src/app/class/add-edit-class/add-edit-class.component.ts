import { Component, Input, OnInit } from '@angular/core';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-add-edit-class',
  templateUrl: './add-edit-class.component.html',
  styleUrls: ['./add-edit-class.component.css']
})
export class AddEditClassComponent implements OnInit {

  @Input() class:any
  classID: number = 0
  className: string = ""
  daysOfWeek: string = ""
  location: string = ""
  classStartTime: string = ""
  classEndTime: string = ""
  classDateStart: Date = new Date()
  classDateEnd: Date = new Date()
  
  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.classID = this.class.classID
    this.className = this.class.className
    this.daysOfWeek = this.class.daysOfWeek
    this.location = this.class.location
    this.classStartTime = this.class.classStartTime
    this.classEndTime = this.class.classEndTime
    this.classDateStart = this.class.classDateStart
    this.classDateEnd = this.class.classDateEnd
  }

  addClass(){
    var cls = {
      className:this.className,
      daysOfWeek:this.daysOfWeek,
      location:this.location,
      classStartTime:this.classStartTime,
      classEndTime:this.classEndTime,
      classDateStart: this.classDateStart,
      classDateEnd: this.classDateEnd
    }

    this.service.addClass(cls).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-class-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showAddSuccess = document.getElementById('add-class-success-alert');
      if(showAddSuccess) {
        showAddSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showAddSuccess) {
          showAddSuccess.style.display = "none"
        }
      }, 4000);
    })
  }

  updateClass(){

  }
}
