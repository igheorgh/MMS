import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskModel } from 'src/app/shared/models/taskModel';
import { TaskService } from 'src/app/shared/services/task.service';
import { CustomLoaderService } from 'src/app/shared/services/customLoader.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
    selector: 'app-task-create',
    templateUrl: 'task-create.component.html'
})

export class TaskCreateComponent implements OnInit {
    public taskForm: FormGroup;
    public otherErrorsDiv = null;
    public otherErrorsDivPassword = null;
    public otherErrorsDivTwofa = null;
    constructor(public taskService: TaskService, private formBuilder: FormBuilder, private router: Router,
        private customService: CustomLoaderService, private userService: UserService) { 
            this.taskService.selectedTaskChanged.subscribe(_ => {
                this.reinitForms();
            });
        }

    ngOnInit() {
        this.reinitForms();
    }
    get f() { return this.taskForm.controls; }
    checkInput(input: any) {
        return input.invalid && (input.dirty || input.touched);
    }

    reinitForms(clear: boolean = false) {
        if(clear) this.taskService.selectedTask = null;
        this.taskForm = this.formBuilder.group({
            taskName: [this.taskService.selectedTask?.name, [Validators.required]],
            description: [this.taskService.selectedTask?.description, [Validators.required]],
            status: [this.taskService.selectedTask?.status, [Validators.required]],
        });
    }

    public atLeastOneEdit: boolean = false;
    onSubmit() {
        this.taskForm.markAllAsTouched();
        if (this.taskForm.invalid && this.taskService.selectedTask == null) {
            return;
        }
        if (this.taskService.selectedTask != null) {
            this.customService.start();
            this.taskService.updateTask(<TaskModel>{
                id: this.taskService.selectedTask.id,
                name: this.f.taskName.value,
                description: this.f.description.value,
                status: this.f.status.value,
            }).subscribe(r => {
                this.otherErrorsDiv = "";
                this.reinitForms();
                this.customService.success('Taskul a fost actualizat!', 'Success');
                this.taskService.hideCreateForm();
                // this.taskService.getAllTasks().subscribe(resp => {
                //     this.customService.stop();
                // }, this.customService.errorFromResp)
            }, this.customService.errorFromResp);
        }
        else {
            this.customService.start();
            this.taskService.createTask(<TaskModel>{
                name: this.f.taskName.value,
                description: this.f.description.value,
                status: this.f.status.value
            }).subscribe(response => {
                this.otherErrorsDiv = "";
                this.reinitForms();
                this.customService.success('Taskul a fost creat!', 'Success');
                this.taskService.hideCreateForm();
                // this.taskService.getAllTasks().subscribe(resp => {
                //     this.customService.stop();
                // }, this.customService.errorFromResp);
            }, this.customService.errorFromResp);
        }
    }
    
}