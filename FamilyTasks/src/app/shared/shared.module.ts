import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TextMaskModule } from 'angular2-text-mask/src/angular2TextMask';
import { AtomsInputComponent } from './components/atoms/atoms-input/atoms-input.component';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        //FontAwesomeModule,
        RouterModule,
        NgbModule,
        MaterialModule,
        QuillModule.forRoot(),
        NgSelectModule,
        TextMaskModule,
    ],
    exports: [
        AtomsInputComponent
    ],
    declarations: [SharedComponent],
    providers: [],
})
export class SharedModule { }
