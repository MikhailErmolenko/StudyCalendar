import {Component, OnInit} from '@angular/core';
import {EmailNotificationService} from '../../../../core/service/email-notification.service';

@Component({
  selector: 'app-email-notification',
  templateUrl: './email-notification.component.html',
  styleUrls: ['./email-notification.component.scss']
})
export class EmailNotificationComponent implements OnInit {

  constructor(public emailNotificationService: EmailNotificationService) {
  }

  ngOnInit(): void {
  }

  toggleSubscription() {
    this.emailNotificationService.toggleSubscribe();
  }
}
