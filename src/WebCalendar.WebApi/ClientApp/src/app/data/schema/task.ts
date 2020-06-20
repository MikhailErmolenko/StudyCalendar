import {Calendar} from './calendar';

export class Task {
  id?: string;
  title?: string;
  description?: string;
  startTime?: Date;
  isDone?: boolean;
  calendarId?: string;
  calendar?: Calendar;
}
