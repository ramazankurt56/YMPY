import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
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
