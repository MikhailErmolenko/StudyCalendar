import {Component, Input, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import { FullCalendarComponent } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import {EventInput} from '@fullcalendar/core/structs/event';
import {Calendar} from '../../../../data/schema/calendar';
import { TaskService } from '../../../../data/service/task.service';
import { EventService } from '../../../../data/service/event.service';
import {OutsidePlacement, RelativePosition, Toppy, ToppyControl} from 'toppy';
import {forkJoin, Observable} from "rxjs";
import { Task } from "../../../../data/schema/task";
import ukLocale from '@fullcalendar/core/locales/uk';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {

  @Input() calendars: Array<Calendar>;

  @ViewChild('calendar') calendarComponent: FullCalendarComponent;

  currentEventsPopover: ToppyControl;
  currentTaskId: string;
  @ViewChild('taskPopover') taskPopoverRef: TemplateRef<any>;

  calendarPlugins = [timeGridPlugin, dayGridPlugin, interactionPlugin];

  calendarSettings = {
    timeZone: 'Europe/Kiev',
    height: 'parent',
    firstDay: 1,
    locale: ukLocale,
    eventTimeFormat: {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    },
    nowIndicator: true,
    header: {
      left: 'timeGridDay,timeGridWeek,dayGridMonth',
      center: 'title',
      right: 'today prev,next'
    }

  };

  calendarEvents = [
    //ПН
    {
      title: 'День самостійної роботи',
      start: '2020-06-22'
    },

    //ВТ
    {
      title: 'Проектування інформаційних систем (Л), доц. Долгов В.М. 12/412',
      start: '2020-06-23T09:30:00',
      end: '2020-06-23T10:50:00'
    },
    {
      title: 'Додаткові розділи з програмування (Л), ст. викл. Єгоров А.О. 12/412',
      start: '2020-06-23T11:10:00',
      end: '2020-06-23T12:30:00'
    },
    {
      title: 'Додаткові розділи з програмування (ПР), ст. викл. Єгоров А.О. 12/412',
      start: '2020-06-23T12:40:00',
      end: '2020-06-23T14:00:00'
    },

    //СР
    {
      title: 'Управління IT-проектами (Л), проф. Олевський В.І. 12/412',
      start: '2020-06-24T09:30:00',
      end: '2020-06-24T10:50:00'
    },
    {
      title: 'Проектування інформаційних систем (Л.Р.), доц. Долгов В.М. 12/412',
      start: '2020-06-24T11:10:00',
      end: '2020-06-24T12:30:00'
    },
    {
      title: 'Управління IT-проектами (Л.Р.), проф. Олевський В.І. 12/302а',
      start: '2020-06-24T12:40:00',
      end: '2020-06-24T14:00:00'
    },
    {
      title: 'Управління IT-проектами (Л.Р.), проф. Олевський В.І. 12/302а',
      start: '2020-06-24T14:10:00',
      end: '2020-06-24T15:30:00'
    },

    //ЧТ
    {
      title: 'Фізична культура',
      start: '2020-06-25T09:30:00',
      end: '2020-06-25T10:50:00'
    },
    {
      title: 'Крос-платформне програмування (Л), доц. Волковський О.С. 12/412',
      start: '2020-06-25T11:10:00',
      end: '2020-06-25T12:30:00'
    },
    {
      title: 'Крос-платформне програмування (ПР), доц. Волковський О.С. 12/412',
      start: '2020-06-25T12:40:00',
      end: '2020-06-25T14:00:00'
    },

    //ПТ
    {
      title: 'Додаткові розділи з програмування (Л.Р.), ст. викл. Єгоров А.О. 12/302а',
      start: '2020-06-26T08:00:00',
      end: '2020-06-26T09:20:00'
    },
    {
      title: 'Додаткові розділи з програмування (Л), ст. викл. Єгоров А.О. 12/412',
      start: '2020-06-26T09:30:00',
      end: '2020-06-26T10:50:00'
    },
    {
      title: 'Крос-платформне програмування (Л.Р.), асист. Обидений Є.О. 12/302',
      start: '2020-06-26T11:10:00',
      end: '2020-06-26T12:30:00'
    },

    //СБ
    {
      title: 'День самостійної роботи',
      start: '2020-06-27'
    }
  ]

  //calendarEvents: EventInput[] = [];

  constructor(
    private router: Router,
    private taskService: TaskService,
    private toppy: Toppy,
    private eventService: EventService
  ) {
  }

  ngOnInit(): void {
    // this.updateEvents();
    // let calendarApi = this.calendarComponent.getApi();
  }

  eventRender($event: any) {
    $event.el.style.cursor = 'pointer';
    if ($event.event.extendedProps.isDone) {
      $event.el.style.textDecoration = 'line-through';
      $event.el.style.backgroundColor = 'gray';
    }
  }

  // bad way
  public updateEvents(calendars: Array<Calendar>) {
    this.calendars = calendars;
    //this.calendarEvents = [];
    const newEventsObservable: Array<Observable<Array<Task>>> = [];
    this.calendars.forEach(calendar => {
      newEventsObservable.push(this.taskService.getAllTasks(calendar.id));
    });

    forkJoin(newEventsObservable).subscribe( value => {
      let tasks: Array<Task> = [];
      value.forEach(v => {
        tasks = tasks.concat(v);
      });

      //this.calendarEvents = tasks.map(v => {
      //  const event: EventInput = {
      //    id: v.id,
      //    title: v.title,
      //    date: v.startTime,
      //    textColor: 'white',
      //    borderColor: '#00a9ff',
      //    backgroundColor: '#00a9ff',
      //    isDone: v.isDone
      //  };
      //  return event;
      //});

      if (this.currentEventsPopover) {
        this.currentEventsPopover.close();
      }
    });

    /*this.taskService.getAllTasks(this.calendars[0].id).subscribe(tasks => {
      this.calendarEvents = tasks.map(value => {
        const event: EventInput = {
          id: value.id,
          title: value.title,
          date: value.startTime,
          textColor: 'white',
          borderColor: '#00a9ff',
          backgroundColor: '#00a9ff',
          isDone: value.isDone
        };
        return event;
      });
    });*/
  }

  //public updateEvents2(calendars: Array<Calendar>) {
  //  this.calendars = calendars;
  //  this.calendarEvents = [];
  //  const newEventsObservable: Array<Observable<Array<Event>>> = [];
  //  this.calendars.forEach(calendar => {
  //    newEventsObservable.push(this.eventService.getAllEvents(calendar.id));
  //  });

  //  forkJoin(newEventsObservable).subscribe(value => {
  //    let events: Array<Event> = [];
  //    value.forEach(v => {
  //      events = events.concat(v);
  //    });

  //    this.calendarEvents = events.map(v => {
  //      const event: EventInput = {
  //        id: v.id,
  //        title: v.title,
  //        date: v.startTime,
  //        textColor: 'white',
  //        borderColor: '#00a9ff',
  //        backgroundColor: '#00a9ff',
  //      };
  //      return event;
  //    });

  //    if (this.currentEventsPopover) {
  //      this.currentEventsPopover.close();
  //    }
  //  });

    /*this.taskService.getAllTasks(this.calendars[0].id).subscribe(tasks => {
      this.calendarEvents = tasks.map(value => {
        const event: EventInput = {
          id: value.id,
          title: value.title,
          date: value.startTime,
          textColor: 'white',
          borderColor: '#00a9ff',
          backgroundColor: '#00a9ff',
          isDone: value.isDone
        };
        return event;
      });
    });*/

  onEventClick($event: any) {
    if (this.currentEventsPopover) {
      this.currentEventsPopover.close();
    }

    this.currentTaskId = $event.event.id;

    const position = new RelativePosition({
      placement: OutsidePlacement.TOP,
      src: $event.el,
    });
    console.log($event);
    const overlay = this.toppy
      .position(position)
      .config({
        containerClass: 'event-popup-container',
        wrapperClass: 'event-popup-wrapper'
      })
      .content(this.taskPopoverRef)
      .create();

    this.currentEventsPopover = overlay;

    overlay.listen('t_close').subscribe(value => {
      this.updateEvents(this.calendars);
    });

    overlay.open();
  }

  onDateClick($event: any) {
    if (this.currentEventsPopover) {
      this.currentEventsPopover.close();
    }
  }
}
