import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaskModel } from '../models/taskModel';
import { Observable, Subject } from 'rxjs';
import { AppSettings } from 'src/app/app.settings';
import { map } from 'rxjs/operators';

export class TaskChangedModel {
    task: TaskModel;
    showCreate: boolean;
}

@Injectable()
export class TaskService {

    public taskList: TaskModel[] = [];
    public selectedTask: TaskModel;
    public createFormVisible: boolean = false;

    public selectedTaskChanged: Subject<TaskChangedModel>;
    constructor(private appSettings: AppSettings, private http: HttpClient){
        this.selectedTaskChanged = new Subject<TaskChangedModel>();
    }
    
    public showCreateForm(selectedTask: TaskModel = null){
        this.selectedTask = selectedTask;
        this.createFormVisible = true;
        this.selectedTaskChanged.next({
            showCreate: true,
            task: selectedTask
        });
    }

    public hideCreateForm(){
        this.selectedTask = null;
        this.createFormVisible = false;
        this.selectedTaskChanged.next({
            showCreate: false,
            task: null
        });
    }

    public deleteTask(task: TaskModel): Observable<any>{
        return this.http.delete(this.appSettings.baseApiUrl + "task/" + task.id);
    }

    public getAllTasks(): Observable<TaskModel[]>{
        return this.http.get<TaskModel[]>(this.appSettings.baseApiUrl + "task/all").pipe(map(list => {
            this.taskList = list;
            return list;
        }));
    }

    public updateTask(task: TaskModel): Observable<TaskModel>{
        return this.http.put<TaskModel>(this.appSettings.baseApiUrl + "task", task).pipe(map(x => {
            this.selectedTask = x;
            return x;
        }));
    }

    public createTask(task: TaskModel): Observable<TaskModel>{
        return this.http.post<TaskModel>(this.appSettings.baseApiUrl + "task", task).pipe(map(x => {
            this.selectedTask = x;
            return x;
        }));
    }
}