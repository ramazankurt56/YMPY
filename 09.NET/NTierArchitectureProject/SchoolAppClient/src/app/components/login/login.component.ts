import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,FormValidateDirective ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginModel = new LoginModel();

  constructor(
    private http: HttpClient,
    private router: Router
  ){    
  }

  login(form:NgForm){
    if(form.valid){
      this.http.post("https://localhost:7135/api/Users/Login",this.model).subscribe({
        next: (res:any)=> {
          localStorage.setItem("response",res.response);
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse)=> {
          console.log(err);          
        }
      });
    }
  }
}
