import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-timepicker',
  templateUrl: './timepicker.component.html',
  styleUrls: ['./timepicker.component.scss']
})
export class TimepickerComponent implements OnInit {

  @Output() timeEmitter: EventEmitter<{ hour: number, minute: number }>;

  time: {
    hour: number,
    minute: number
  };
  spinners = false;

  constructor() {
    this.timeEmitter = new EventEmitter<{ hour: number, minute: number }>();
  }

  ngOnInit(): void {
    const currentTime = new Date();
    this.time = {
      hour: currentTime.getHours(),
      minute: currentTime.getMinutes()
    };
    this.timeEmitter.emit(this.time);
  }


  onModelChange() {
    this.timeEmitter.emit(this.time);
  }
}
