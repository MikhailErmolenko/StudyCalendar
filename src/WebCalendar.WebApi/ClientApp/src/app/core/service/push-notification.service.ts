import {Injectable, NgZone} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {AuthenticationService} from './authentication.service';
import {map, mergeMapTo} from 'rxjs/operators';
import {AngularFireMessaging} from '@angular/fire/messaging';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PushNotificationService {

  public pushNotificationStatus = {
    isSubscribe: false,
    inProgress: false
  };
  public pushNotificationStatusSubject = new BehaviorSubject<{
    isSubscribe: boolean,
    inProgress: boolean
  }>(this.pushNotificationStatus);


  constructor(
    private http: HttpClient,
    private authenticationService: AuthenticationService,
    private afMessaging: AngularFireMessaging,
    private zone: NgZone) {

    this.checkSubscribe();

    // replace by in-app notify
    afMessaging.onMessage(payload => {
      console.log(payload);
      const title = payload.notification.title;
      const options = {
        body: payload.notification.body,
        icon: payload.notification.icon
      };
      const notification = new Notification(title, options);
    });
  }

  checkSubscribe() {
    this.pushNotificationStatus.isSubscribe = localStorage.getItem('pushNotificationToken') != null;
    this.pushNotificationStatusSubject.next(this.pushNotificationStatus);
  }

  init() {
    this.pushNotificationStatus.inProgress = true;
    this.pushNotificationStatusSubject.next(this.pushNotificationStatus);

    this.afMessaging.requestPermission
      .pipe(mergeMapTo(this.afMessaging.tokenChanges))
      .subscribe(
        (token) => {
          console.log('Permission granted! Save to the server!', token);

          localStorage.setItem('pushNotificationInit', JSON.stringify(true));
          this.pushSubscribe().subscribe();
        },
        (error) => {
          console.error(error);
        },
      );
  }

  isSubscribeInit(): boolean {
    return localStorage.getItem('pushNotificationInit') != null;
  }

  pushSubscribe(): Observable<any> {

    const currentUser = this.authenticationService.currentUserValue;

    return this.afMessaging.getToken.pipe(map(token => {
      localStorage.setItem('pushNotificationToken', JSON.stringify(token));
      this.zone.run(() => this.checkSubscribe());

      this.http.post(`${environment.apiUrl}/notification/push/subscribe/${currentUser.id}`,
        {deviceToken: token})
        .subscribe(result => {
          this.zone.run(() => {
            this.pushNotificationStatus.inProgress = false;
            this.pushNotificationStatusSubject.next(this.pushNotificationStatus);
          });
        });
    }));
  }

  pushUnsubscribe(): Observable<any> {
    this.pushNotificationStatus.inProgress = true;
    this.pushNotificationStatusSubject.next(this.pushNotificationStatus);

    const currentUser = this.authenticationService.currentUserValue;

    return this.afMessaging.getToken.pipe(map(token => {
      this.afMessaging.deleteToken(token);
      localStorage.removeItem('pushNotificationToken');
      this.checkSubscribe();

      this.http.post(`${environment.apiUrl}/notification/push/unsubscribe/${currentUser.id}`,
        {deviceToken: token})
        .subscribe(result => {
          this.zone.run(() => {
            this.pushNotificationStatus.inProgress = false;
            this.pushNotificationStatusSubject.next(this.pushNotificationStatus);
          });
        });
    }));
  }

  toggleSubscription() {
    if (this.pushNotificationStatus.isSubscribe) {
      this.pushUnsubscribe().subscribe();
    } else {
      this.init();
    }
  }
}
