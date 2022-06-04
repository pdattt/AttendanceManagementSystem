import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AttendanceManagementService } from 'src/app/attendance-management.service';

@Component({
  selector: 'app-show-class',
  templateUrl: './show-class.component.html',
  styleUrls: ['./show-class.component.css']
})
export class ShowClassComponent implements OnInit {

  classes!: Observable<any[]>
  class: any
  modalTitle:string = ""
  activateAddEditClassComponent:boolean = false;
  daysOfWeek: string = ""

  constructor(private service: AttendanceManagementService) { }

  ngOnInit(): void {
    this.classes = this.service.getAllClasses()
  }

  getDaysOfWeek(id: number){
    this.daysOfWeek = this.service.getDaysOfWeek(id)
  }
  
  modalAdd(){
    this.class = {
      classID: 0,
      className:null,
      classStartTime:null,
      classEndTime:null,
      classDateStart:null,
      classDateEnd:null,
      classDaysOfWeek:null
    }
    this.modalTitle = "Add Class";
    this.activateAddEditClassComponent = true;
  }

  modalEdit(cls: any){
    this.class = cls;
    this.modalTitle = "Edit Class";
    this.activateAddEditClassComponent = true;
  }

  delete(cls: any){
    if(confirm(`Are you sure you want to delete this class with code ${cls.classID}`)) {
      this.service.deleteClass(cls.eventID).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-class-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showDeleteSuccess = document.getElementById('delete-class-success-alert');
      if(showDeleteSuccess) {
        showDeleteSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showDeleteSuccess) {
          showDeleteSuccess.style.display = "none"
        }
      }, 4000);
      this.classes = this.service.getAllClasses();
      })
    }
  }

  modalClose(){
    this.activateAddEditClassComponent = false;
    this.classes = this.service.getAllClasses()
  }
}
