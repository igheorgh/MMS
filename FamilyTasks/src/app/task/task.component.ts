import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from 'src/app/shared/services/task.service';
import { SprintService } from '../shared/services/sprint.service';

@Component({
    selector: 'app-task',
    templateUrl: 'task.component.html'
})

export class TaskComponent implements OnInit {
    
    public createMode: boolean = false;

    constructor(public taskService: TaskService, public sprintService: SprintService, public router: Router) {
        this.taskService.selectedTaskChanged.subscribe(data => {
            this.createMode = data.showCreate;
        });
     }

    ngOnInit() {
        if(this.sprintService.selectedSprint == null){
            this.router.navigate(['/sprints']);
        }
        this.createMode = this.taskService.createFormVisible;
    }
}