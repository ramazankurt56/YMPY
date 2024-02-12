import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template:"<router-outlet></router-outlet>"
})
export class AppComponent {
name:string="Ramazan"
name2:string=""
names:string[]=["ramazan","Ela"]
showName2InConsole(){
  this.name2=this.name
  console.log(this.name2)
}
}
