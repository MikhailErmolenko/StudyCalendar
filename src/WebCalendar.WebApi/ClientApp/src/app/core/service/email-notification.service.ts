import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthenticationService} from './authentication.service';
import {environment} from '../../../environments/environment';
import {BehaviorSubject, Observable} from 'rxjs';
import {finalize, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EmailNotificationService {

  public emailNotificationStatus = {
    isSubscribe: false,
    inProgress: false
  };
  public emailNotificationStatusSubject = new BehaviorSubject<{
    isSubscribe: boolean,
    inProgress: boolean
  }>(this.emailNotificationStatus);

  constructor(private http: HttpClient,
              private authenticationService: AuthenticationService) {
    const currentUser = this.authenticationService.currentUserValue;
    this.http.get<boolean>(`${environment.apiUrl}/notification/email/isSubscribe/${currentUser.id}`)
      .subscribe(isSubscribe => {
        this.emailNotificationStatus.isSubscribe = isSubscribe;
        this.emailNotificationStatusSubject.next(this.emailNotificationStatus);
      });
  }

  unsubscribe(): Observable<any> {
    this.emailNotificationStatus.inProgress = true;
    const currentUser = this.authenticationService.currentUserValue;
    return this.http.put(`${environment.apiUrl}/notification/email/unsubscribe/${currentUser.id}`, null)
      .pipe(
        map(() => {
          this.emailNotificationStatus.isSubscribe = false;
        }),
        finalize(() => {
          this.emailNotificationStatus.inProgress = false;
          this.emailNotificationStatusSubject.next(this.emailNotificationStatus);
        }));
  }

  subscribe(): Observable<any> {
    this.emailNotificationStatus.inProgress = true;
    const currentUser = this.authenticationService.currentUserValue;
    return this.http.put(`${environment.apiUrl}/notification/email/subscribe/${currentUser.id}`, null)
      .pipe(
        map(response => {
          this.emailNotificationStatus.isSubscribe = true;
        }),
        finalize(() => {
          this.emailNotificationStatus.inProgress = false;
          this.emailNotificationStatusSubject.next(this.emailNotificationStatus);
        }));
  }

  toggleSubscribe() {
    if (this.emailNotificationStatus.isSubscribe) {
      this.unsubscribe().subscribe();
    } else {
      this.subscribe().subscribe();
    }
  }
}
