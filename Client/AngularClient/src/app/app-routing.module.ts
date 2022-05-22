import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendeeComponent } from './attendee/attendee.component';

const routes: Routes = [
  {
    path:'',
    component: AttendeeComponent
  },
  {
    path:'attendee',
    component: AttendeeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
