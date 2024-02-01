import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ShoppingCartService } from '../../../services/shopping-cart.service';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  language:string="en"
  constructor(
    private translate:TranslateService,
    public shopping:ShoppingCartService,
    public auth:AuthService,
    private router:Router
    ) {
      if(localStorage.getItem("language"))
      {
        this.language=localStorage.getItem("language") as string;
      }
      translate.setDefaultLang(this.language)
  }

  switchLanguage(event:any){
    localStorage.setItem("language",event.target.value);
    this.language=event.target.value
    this.translate.use(this.language);
    location.reload();
  }
  logout(){
    localStorage.removeItem("response");
    this.shopping.getAllShoppingCarts();
    
    this.router.navigateByUrl("/login")
  }
}
