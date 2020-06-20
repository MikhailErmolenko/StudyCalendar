import {Injectable} from '@angular/core';
import {AuthenticationService} from '../../core/service/authentication.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {Calendar} from '../schema/calendar';

@Injectable()
export class CalendarService {

  constructor(
    private authenticationService: AuthenticationService,
    private http: HttpClient) {
  }

  getUserCalendars(): Observable<Array<Calendar>> {
    const user = this.authenticationService.currentUserValue;
    return this.http.get<Array<Calendar>>(`${environment.apiUrl}/calendar/${user.id}`);
  }
}
