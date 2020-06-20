import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {PushNotificationService} from '../../../../core/service/push-notification.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ActivityModalComponent} from '../../components/activity-modal/activity-modal.component';
import {CalendarService} from '../../../../data/service/calendar.service';
import {Calendar} from '../../../../data/schema/calendar';
import {CalendarComponent} from '../calendar/calendar.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit, AfterViewInit {

  @ViewChild('calendarComponent') calendarComponent: CalendarComponent;

  userCalendars: Array<Calendar>;

  constructor(private pushNotificationService: PushNotificationService,
              private modalService: NgbModal,
              private calendarService: CalendarService) {
  }

  ngOnInit(): void {
    if (!this.pushNotificationService.isSubscribeInit()) {
      this.pushNotificationService.init();
    }

    this.calendarService.getUserCalendars().subscribe(calendars => {
      this.userCalendars = calendars;
      this.calendarComponent.updateEvents(calendars);
    });
  }

  openAddEventModal() {
    const activityModalRef = this.modalService.open(ActivityModalComponent);
    activityModalRef.componentInstance.userCalendars = this.userCalendars;
    activityModalRef.result.then(value => {
      if (value === 'save') {
        this.calendarComponent.updateEvents(this.userCalendars);
      }
    });
  }

  ngAfterViewInit(): void {
  }
}
