import {Component, OnInit} from '@angular/core';
import {PushNotificationService} from '../../../../core/service/push-notification.service';

@Component({
  selector: 'app-push-notification',
  templateUrl: './push-notification.component.html',
  styleUrls: ['./push-notification.component.scss']
})
export class PushNotificationComponent implements OnInit {

  constructor(public pushNotificationService: PushNotificationService) {

  }

  ngOnInit(): void {
  }

  toggleSubscription() {
    this.pushNotificationService.toggleSubscription();
  }
}
