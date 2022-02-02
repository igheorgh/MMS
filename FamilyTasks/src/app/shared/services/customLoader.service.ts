import { Injectable } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class CustomLoaderService {
    constructor(private loaderService: NgxUiLoaderService, private toastr: ToastrService) {
    }
    
     public start = () => {
         this.loaderService.start();
     }

     public stop = () => {
         this.loaderService.stop();
     }

     public success = (message: string, title: string) => {
         this.toastr.success('', message);
     }

     public error =  (message: string, title: string) => {
         this.toastr.error(message, title);
     }

     public errorFromResp =  (error: any) => {
            this.stop();
            this.error(error.error.message, 'Cererea a esuat!');
     }

     public defaultError =  (_) => {
        this.stop();
        this.error('Ceva nu a functionat, va rugam sa reincercati!', 'Oops...');
     }
}