import { Injectable } from '@angular/core';
import { AuthenticationService } from '../../core/service/authentication.service';
import { HttpClient } from '@angular/common/http';
import { Event } from '../schema/event';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
@Injectable()
export class EventService {
  constructor(private authenticationService: AuthenticationService, private http: HttpClient) {
  }
  createEvent(event: Event): Observable<Event> {
    const user = this.authenticationService.currentUserValue;
    return this.http.post<Event>(`${environment.apiUrl}/event/${user.id}`, event)
      .pipe(map(e => {
        e.startTime = new Date(e.startTime);
        return e;
      }));
  }
  getAllEvents(calendarId: string): Observable<Array<Event>> {
    const user = this.authenticationService.currentUserValue;
    return this.http.get<Array<Event>>(`${environment.apiUrl}/event/all/${calendarId}/${user.id}`)
      .pipe(map(events => {
        events.forEach((value, index, array) => {
          array[index].startTime = new Date(value.startTime + 'Z');
        });
        return events;
      }));
  }
  getEventById(eventId: string): Observable<Event> {
    const user = this.authenticationService.currentUserValue;
    return this.http.get<Event>(`${environment.apiUrl}/event/${eventId}/${user.id}`)
      .pipe(map(event => {
        event.startTime = new Date(event.startTime + 'Z');
        return event;
      }));
  }
  deleteEvent(eventId: string): Observable<any> {
    const user = this.authenticationService.currentUserValue;
    return this.http.delete(`${environment.apiUrl}/event/${eventId}/${user.id}`);
  }
}

