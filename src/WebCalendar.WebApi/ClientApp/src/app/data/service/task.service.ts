import {Injectable} from '@angular/core';
import {AuthenticationService} from '../../core/service/authentication.service';
import {HttpClient} from '@angular/common/http';
import {Task} from '../schema/task';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {map} from 'rxjs/operators';

@Injectable()
export class TaskService {

  constructor(
    private authenticationService: AuthenticationService,
    private http: HttpClient
  ) {
  }

  createTask(task: Task): Observable<Task> {
    const user = this.authenticationService.currentUserValue;
    return this.http.post<Task>(`${environment.apiUrl}/task/${user.id}`, task)
      .pipe(map(t => {
        t.startTime = new Date(t.startTime);
        return t;
      }));
  }

  getAllTasks(calendarId: string): Observable<Array<Task>> {
    const user = this.authenticationService.currentUserValue;
    return this.http.get<Array<Task>>(`${environment.apiUrl}/task/all/${calendarId}/${user.id}`)
      .pipe(map(tasks => {
        tasks.forEach((value, index, array) => {
          array[index].startTime = new Date(value.startTime + 'Z');
        });
        return tasks;
      }));
  }

  getTaskById(taskId: string): Observable<Task> {
    const user = this.authenticationService.currentUserValue;
    return this.http.get<Task>(`${environment.apiUrl}/task/${taskId}/${user.id}`)
      .pipe(map(task => {
        task.startTime = new Date(task.startTime + 'Z');
        return task;
      }));
  }

  checkTask(taskId: string): Observable<any> {
    return this.toggleTaskCompletion({
      id: taskId,
      isDone: true
    });
  }

  uncheckTask(taskId: string): Observable<any> {
    return this.toggleTaskCompletion({
      id: taskId,
      isDone: false
    });
  }

  deleteTask(taskId: string): Observable<any> {
    const user = this.authenticationService.currentUserValue;
    return this.http.delete(`${environment.apiUrl}/task/${taskId}/${user.id}`);
  }

  private toggleTaskCompletion(task: Task): Observable<any> {
    const user = this.authenticationService.currentUserValue;
    return this.http.put(`${environment.apiUrl}/task/completion/${task.id}/${user.id}`, task);
  }
}
