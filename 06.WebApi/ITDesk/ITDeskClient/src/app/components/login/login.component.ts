import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { DividerModule } from 'primeng/divider';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CardModule,ButtonModule,InputTextModule,PasswordModule,FormsModule,DividerModule,ToastModule],
  providers:[MessageService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  /**
   *
   */
  constructor(private messageService:MessageService) {}
userNameOrEmail:string=""
password:string=""


signIn() {
  if(this.userNameOrEmail.length<3){
  this.messageService.add({ severity: 'warn', summary: 'Validasyon Hatası', detail: 'Geçerli bir Kullanıcı adı yada mail giriniz' });
  return
}
if(this.password.length<6){
  this.messageService.add({ severity: 'warn', summary: 'Validasyon Hatası', detail: 'Şifreniz en az 6 karakter olmalıdır.' });
  return
}
}
}
