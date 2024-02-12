import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { C1Component } from './c1/c1.component';
import { C2Component } from './c2/c2.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path:"",
    component:AppComponent,
    children:[
      {path:"c1",component:C1Component},{path:"c2",component:C2Component}
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
