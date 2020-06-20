import { Calendar } from './calendar';

export class Event {
  id?: string;
  title?: string;
  description?: string;
  startTime?: Date;
  endTime?: Date;
  calendarId?: string;
  calendar?: Calendar;
}
