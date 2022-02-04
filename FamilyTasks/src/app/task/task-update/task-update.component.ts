import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskModel } from 'src/app/shared/models/taskModel';
import { CustomLoaderService } from 'src/app/shared/services/customLoader.service';
import { TaskService } from 'src/app/shared/services/task.service';
import { TaskListComponent } from '../task-list/task-list.component';

@Component({
    selector: 'app-task-update',
    templateUrl: 'task-update.component.html',
    styleUrls: ['./style-task.component.scss']
})

export class TaskUpdateComponent implements OnInit {
    id: string
    task : TaskModel
    form: FormGroup;
    constructor(public taskService: TaskService,private formBuilder: FormBuilder, public router: Router,
       private route :ActivatedRoute, private customService: CustomLoaderService) {
      
     }

    ngOnInit() {
      this.createForm();
      this.route.params.subscribe(res=> this.id = res["id"]);
      this.taskService.GetTaskById(this.id).subscribe(res=>{
        this.task = res;
        console.log(this.task)
        this.initializareForm(this.task)
      })
    }

    createForm(){
      this.form = this.formBuilder.group({
        status: [''],
        description: [''],
        name: [''],
    })
  }
  initializareForm(task:TaskModel){
    this.form.patchValue({status:task.status});
    this.form.patchValue({description:task.description});
    this.form.patchValue({name:task.name});
  }
  // UpdateStatus(){
  //   this.task.status = this.form.get("status").value
  //   if(this.task.status == "InProgress"){
  //     this.taskService.InProgressStatus(this.task).subscribe(res=>{})
  //   //  this.router.navigate(['/tasks']);
  //   }
  //   if(this.task.status == "Done"){
  //     this.taskService.DoneStatus(this.task).subscribe(res=>{})
  //    // this.router.navigate(['/tasks']);
  //   }
  //   if(this.task.status == "ToDo"){
  //     this.taskService.ToDoStatus(this.task).subscribe(res=>{})
  //    // this.router.navigate(['/tasks']);
  //   }
  // }

  statusChanged(event){
    this.task.status = event;
    if(this.task.status == "InProgress"){
      this.taskService.InProgressStatus(this.task).subscribe(res=>{this.customService.success('Taskul a fost actualizat!', 'Success');})
    }
    if(this.task.status == "Done"){
      this.taskService.DoneStatus(this.task).subscribe(res=>{this.customService.success('Taskul a fost actualizat!', 'Success');})
    }
    if(this.task.status == "ToDo"){
      this.taskService.ToDoStatus(this.task).subscribe(res=>{this.customService.success('Taskul a fost actualizat!', 'Success');})
    }
  }
}