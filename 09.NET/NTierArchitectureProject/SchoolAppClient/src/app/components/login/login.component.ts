import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  login: LoginModel = new LoginModel()
  constructor(private http: HttpClient, private router: Router) { }
  signIn(form: NgForm) {
    if(form.valid){
    this.http.post("https://localhost:7135/api/Users/Login",this.login)
      .subscribe((res) => {
        if (res.hasOwnProperty('response')) {
          localStorage.setItem("response", JSON.stringify(res));
          this.router.navigateByUrl("/");
        }
        else {
          this.router.navigateByUrl("login");
        }
      })
  }
}
}
