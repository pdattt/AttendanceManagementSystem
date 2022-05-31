import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendeeComponent } from './attendee/attendee.component';
import { ClassComponent } from './class/class.component';
import { EventComponent } from './event/event.component';
import { ManageAttendeeEventComponent } from './event/manage-attendee-event/manage-attendee-event.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path:'',
    component: LoginComponent
  },
  {
    path:'attendee',
    component: AttendeeComponent
  },
  {
    path:'event',
    component: EventComponent
  },
  {
    path:'attendee-in-event/:id',
    component: ManageAttendeeEventComponent
  },
  {
    path:'class',
    component: ClassComponent
  },
  {
    path:'login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
