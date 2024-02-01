import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';


const routes: Routes = [
 
  {
    path:"",
    component:LayoutsComponent,
    children:[
      {
        path:"",
        component:HomeComponent
      },
      {
        path:"shopping-cart",
        component:ShoppingCartComponent
      }, {
        path:"register",
        component:RegisterComponent
      },
      {
        path:"login",
        component:LoginComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
