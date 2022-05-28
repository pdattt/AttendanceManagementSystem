import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendeeComponent } from './attendee/attendee.component';
import { EventComponent } from './event/event.component';
import { ManageAttendeeEventComponent } from './event/manage-attendee-event/manage-attendee-event.component';

const routes: Routes = [
  {
    path:'',
    component: AttendeeComponent
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
