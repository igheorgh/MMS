import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaskModel } from '../models/taskModel';
import { Observable, Subject } from 'rxjs';
import { AppSettings } from 'src/app/app.settings';
import { map } from 'rxjs/operators';
import { TaskListComponent } from 'src/app/task/task-list/task-list.component';
import { TaskStatusStrategy } from '../strategies/tasks/strategies/TaskStatusChangeStartegy';
import { TaskOperationType } from '../strategies/tasks/TaskOperationTypes';
import { TaskContext } from '../strategies/tasks/TaskContext';

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
    constructor(private appSettings: AppSettings, private http: HttpClient) {
        this.selectedTaskChanged = new Subject<TaskChangedModel>();
    }


    public showCreateForm(selectedTask: TaskModel = null) {
        this.selectedTask = selectedTask;
        this.createFormVisible = true;
        this.selectedTaskChanged.next({
            showCreate: true,
            task: selectedTask
        });
    }

    public hideCreateForm() {
        this.selectedTask = null;
        this.createFormVisible = false;
        this.selectedTaskChanged.next({
            showCreate: false,
            task: null
        });
    }

    public deleteTask(task: TaskModel): Observable<any> {
        return this.http.delete(this.appSettings.baseApiUrl + "task/" + task.id);
    }

    public getAllTasks(sprint_id: string): Observable<TaskModel[]> {
        return this.http.get<TaskModel[]>(this.appSettings.baseApiUrl + "task/all/" + sprint_id).pipe(map(list => {
            this.taskList = list;
            return list;
        }));
    }

    public updateTask(task: TaskModel): Observable<TaskModel> {
        return this.http.put<TaskModel>(this.appSettings.baseApiUrl + "task", task).pipe(map(x => {
            this.selectedTask = x;
            return x;
        }));
    }

    public createTask(task: TaskModel): Observable<TaskModel> {
        return this.http.post<TaskModel>(this.appSettings.baseApiUrl + "task", task).pipe(map(x => {
            this.selectedTask = x;
            return x;
        }));
    }

    public GetTaskById(id: string): Observable<TaskModel> {
        return this.http.get<TaskModel>(this.appSettings.baseApiUrl + "task/" + id);
    }

    InProgressStatus(task: TaskModel) {
        return this.http.put(this.appSettings.baseApiUrl + "task/inprogress/" + task.id, task)
    }

    ToDoStatus(task: TaskModel) {
        return this.http.put(this.appSettings.baseApiUrl + "task/todo/" + task.id, task)
    }

    DoneStatus(task: TaskModel) {
        return this.http.put(this.appSettings.baseApiUrl + "task/done/" + task.id, task)
    }

    AddComment(task: TaskModel, comment: string){
        return this.http.post(this.appSettings.baseApiUrl + "comment", {
            task_Id: task.id,
            description: comment
        });
    }

    DeleteComment(commentId: string){
        return this.http.delete(this.appSettings.baseApiUrl + "comment/" + commentId);
    }
}