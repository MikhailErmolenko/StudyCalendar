import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {NgbCalendar, NgbDate} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-datepicker-popup',
  templateUrl: './datepicker-popup.component.html',
  styleUrls: ['./datepicker-popup.component.scss']
})
export class DatepickerPopupComponent implements OnInit {

  @Output() dateEmitter: EventEmitter<NgbDate>;

  date: NgbDate;


  constructor(private calendar: NgbCalendar) {
    this.dateEmitter = new EventEmitter<NgbDate>();
  }

  onModelChange() {
    this.dateEmitter.emit(this.date);
  }

  ngOnInit(): void {
    this.date = this.calendar.getToday();
    this.dateEmitter.emit(this.date);
  }

}
