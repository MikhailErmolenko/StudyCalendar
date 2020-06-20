import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CalendarRoutingModule} from './calendar-routing.module';
import {CalendarComponent} from './pages/calendar/calendar.component';
import {FullCalendarModule} from '@fullcalendar/angular';
import {MainComponent} from './pages/main/main.component';
import {SettingsComponent} from './pages/settings/settings.component';
import {PushNotificationComponent} from './components/push-notification/push-notification.component';
import {EmailNotificationComponent} from './components/email-notification/email-notification.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {DatepickerPopupComponent} from './components/datepicker-popup/datepicker-popup.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TimepickerComponent} from './components/timepicker/timepicker.component';
import {ActivityModalComponent} from './components/activity-modal/activity-modal.component';
import {CalendarService} from '../../data/service/calendar.service';
import { TaskService } from '../../data/service/task.service';
import { EventService } from "../../data/service/event.service";
import {ToppyModule} from 'toppy';
import {TaskPopoverComponent} from './components/popovers/task-popover/task-popover.component';


@NgModule({
  declarations: [
    CalendarComponent,
    PushNotificationComponent,
    MainComponent,
    SettingsComponent,
    EmailNotificationComponent,
    ActivityModalComponent,
    DatepickerPopupComponent,
    TimepickerComponent,
    TaskPopoverComponent
  ],
  imports: [
    CommonModule,
    CalendarRoutingModule,
    FullCalendarModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    ToppyModule
  ],
  providers: [
    CalendarService,
    TaskService,
    EventService
  ]
})
export class CalendarModule {
}
