import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient,
    private swal: SwalService
  ) { }

  get(api: string, callBack: (res:any)=> void) {
    this.http.get(`https://localhost:7155/api/${api}`, 
    ).subscribe({
      next: (res: any) => {
        callBack(res);
       
      },
      error: (err: HttpErrorResponse) => {
        this.swal.callToast(err.message, "error");
        console.log(err);
      }
    })
  }

  post(api: string, body:any,callBack: (res:any)=> void) {
    this.http.post(`https://localhost:7155/api/${api}`,body
    ).subscribe({
      next: (res: any) => {
        callBack(res);
      },
      error: (err: HttpErrorResponse) => {
        this.swal.callToast(err.error.message, "error");
      }
    })
  }
}
