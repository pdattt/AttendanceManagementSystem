import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AttendanceManagementService } from './attendance-management.service';
import { AttendeeComponent } from './attendee/attendee.component';
import { ShowAttendeeComponent } from './attendee/show-attendee/show-attendee.component';
import { AddEditAttendeeComponent } from './attendee/add-edit-attendee/add-edit-attendee.component';
import { EventComponent } from './event/event.component';
import { ShowEventComponent } from './event/show-event/show-event.component';
import { AddEditEventComponent } from './event/add-edit-event/add-edit-event.component';
import { ClassComponent } from './class/class.component';
import { ShowClassComponent } from './class/show-class/show-class.component';
import { AddEditClassComponent } from './class/add-edit-class/add-edit-class.component';
import { ManageAttendeeEventComponent } from './event/manage-attendee-event/manage-attendee-event.component';
import { LoginComponent } from './login/login.component';
import { SessionEventComponent } from './event/session-event/session-event.component';
import { ManageAttendeeClassComponent } from './class/manage-attendee-class/manage-attendee-class.component';
import { SessionClassComponent } from './class/session-class/session-class.component';
import { ReportEventComponent } from './event/report-event/report-event.component';
import { ReportClassComponent } from './class/report-class/report-class.component';

@NgModule({
  declarations: [
    AppComponent,
    AttendeeComponent,
    ShowAttendeeComponent,
    AddEditAttendeeComponent,
    EventComponent,
    ShowEventComponent,
    AddEditEventComponent,
    ClassComponent,
    ShowClassComponent,
    AddEditClassComponent,
    ManageAttendeeEventComponent,
    LoginComponent,
    SessionEventComponent,
    ManageAttendeeClassComponent,
    SessionClassComponent,
    ReportEventComponent,
    ReportClassComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AttendanceManagementService],
  bootstrap: [AppComponent]
})
export class AppModule { }
