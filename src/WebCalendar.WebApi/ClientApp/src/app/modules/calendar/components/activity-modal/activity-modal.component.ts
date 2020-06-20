import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal, NgbDate} from '@ng-bootstrap/ng-bootstrap';
import {Task} from '../../../../data/schema/task';
import {Calendar} from '../../../../data/schema/calendar';
import {TaskService} from '../../../../data/service/task.service';

@Component({
  selector: 'app-activity-modal',
  templateUrl: './activity-modal.component.html',
  styleUrls: ['./activity-modal.component.scss']
})
export class ActivityModalComponent implements OnInit {

  @Input() userCalendars: Array<Calendar>;

  taskContent: Task;
  activity = 'task';

  constructor(
    public activeModal: NgbActiveModal,
    private taskService: TaskService) {
  }

  ngOnInit(): void {
    this.taskContent = {
      title: '',
      description: '',
      startTime: new Date(),
      calendarId: ''
    };

    console.log(this.userCalendars);
  }

  save() {
    switch (this.activity) {
      case 'task': {
        console.log(this.taskContent);
        this.taskService.createTask(this.taskContent)
          .subscribe(task => {
            console.log(typeof task.startTime);
            console.log(task);
            this.activeModal.close('save');
          });
      }
    }
  }

  setTaskDate($event: NgbDate) {
    this.taskContent.startTime.setFullYear($event.year, $event.month - 1, $event.day);
  }

  setTaskTime($event: { hour: number; minute: number }) {
    this.taskContent.startTime.setHours($event.hour, $event.minute);
  }
}
