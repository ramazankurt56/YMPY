import { Injectable } from '@angular/core';
import Swal from 'sweetalert2'
@Injectable({
  providedIn: 'root'
})
export class SwalService {
  constructor() { }

  callToast(data: string, icon: SweetAlertIcon = "success") {
    console.log(data)
    const Toast = Swal.mixin({
      toast: true,
      position: 'bottom-end',
      timer: 3000,
      timerProgressBar: true,
      showConfirmButton: false,            
    })
    Toast.fire(data, '', icon)
  }
}
export type SweetAlertIcon = 'success' | 'error' | 'warning' | 'info' | 'question'
