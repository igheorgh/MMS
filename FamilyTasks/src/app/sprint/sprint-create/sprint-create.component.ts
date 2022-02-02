import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SprintModel } from 'src/app/shared/models/sprintModel';
import { SprintService } from 'src/app/shared/services/sprint.service';
import { CustomLoaderService } from 'src/app/shared/services/customLoader.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
    selector: 'app-sprint-create',
    templateUrl: 'sprint-create.component.html'
})

export class SprintCreateComponent implements OnInit {
    public sprintForm: FormGroup;
    public otherErrorsDiv = null;
    public otherErrorsDivPassword = null;
    public otherErrorsDivTwofa = null;
    constructor(public sprintService: SprintService, private formBuilder: FormBuilder, private router: Router,
        private customService: CustomLoaderService, private userService: UserService) { 
            this.sprintService.selectedSprintChanged.subscribe(_ => {
                this.reinitForms();
            });
        }

    ngOnInit() {
        this.reinitForms();
    }
    get f() { return this.sprintForm.controls; }
    checkInput(input: any) {
        return input.invalid && (input.dirty || input.touched);
    }

    reinitForms(clear: boolean = false) {
        if(clear) this.sprintService.selectedSprint = null;
        this.sprintForm = this.formBuilder.group({
            sprintName: [this.sprintService.selectedSprint?.name, [Validators.required]],
            sprintCode: [this.sprintService.selectedSprint?.goal, [Validators.required]],
            endDate: [this.sprintService.selectedSprint?.end_Date, [Validators.required]],
            startDate: [this.sprintService.selectedSprint?.start_Date, [Validators.required]]
        });
    }

    public atLeastOneEdit: boolean = false;
    onSubmit() {
        this.sprintForm.markAllAsTouched();
        if (this.sprintForm.invalid && this.sprintService.selectedSprint == null) {
            return;
        }
        if (this.sprintService.selectedSprint != null) {
            this.customService.start();
            this.sprintService.updateSprint(<SprintModel>{
                id: this.sprintService.selectedSprint.id,
                name: this.f.sprintName.value,
                goal: this.f.sprintCode.value,
                end_Date: this.f.endDate.value,
                start_Date: this.f.startDate.value
            }).subscribe(r => {
                this.otherErrorsDiv = "";
                this.reinitForms();
                this.customService.success('Sprintul a fost actualizat!', 'Success');
                this.sprintService.hideCreateForm();
                // this.sprintService.getAllSprints().subscribe(resp => {
                //     this.customService.stop();
                // }, this.customService.errorFromResp)
            }, this.customService.errorFromResp);
        }
        else {
            this.customService.start();
            this.sprintService.createSprint(<SprintModel>{
                name: this.f.sprintName.value,
                goal: this.f.sprintCode.value,
                end_Date: this.f.endDate.value,
                start_Date: this.f.startDate.value
            }).subscribe(response => {
                this.otherErrorsDiv = "";
                this.reinitForms();
                this.customService.success('Sprintul a fost creat!', 'Success');
                this.sprintService.hideCreateForm();
                // this.sprintService.getAllSprints().subscribe(resp => {
                //     this.customService.stop();
                // }, this.customService.errorFromResp);
            }, this.customService.errorFromResp);
        }
    }
    
}