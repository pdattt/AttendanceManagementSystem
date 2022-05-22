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

@NgModule({
  declarations: [
    AppComponent,
    AttendeeComponent,
    ShowAttendeeComponent,
    AddEditAttendeeComponent
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
