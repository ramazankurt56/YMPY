import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SwalService } from '../../services/swal.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

constructor(private http:HttpClient,private swal:SwalService,private router:Router) {}
signUp(form:NgForm){
this.http.post("https://localhost:7215/api/Auth/Register",{
  name:form.controls["name"].value,
  lastname:form.controls["lastname"].value,
  username:form.controls["username"].value,
  email:form.controls["email"].value,
  password:form.controls["password"].value,
}).subscribe((res:any)=>{
  this.swal.callToast(res.message,"success")
  this.router.navigateByUrl("/login")
})
}
}
