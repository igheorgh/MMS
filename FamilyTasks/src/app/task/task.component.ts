import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/shared/services/task.service';

@Component({
    selector: 'app-task',
    templateUrl: 'task.component.html'
})

export class TaskComponent implements OnInit {
    
    public createMode: boolean = false;

    constructor(public taskService: TaskService) {
        this.createMode = this.taskService.createFormVisible;
        this.taskService.selectedTaskChanged.subscribe(data => {
            this.createMode = data.showCreate;
        });
     }

    ngOnInit() { }
}