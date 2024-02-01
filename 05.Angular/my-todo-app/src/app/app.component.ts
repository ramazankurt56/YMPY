import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  @ViewChild("inputEl") element: any;
 work:string="dwd";
 todos:string[]=[];
 update:string=""
 indexUpdate:number=0;
 isUpdateFormActive:Boolean=false

 save(inputEl:any){

  this.todos.push(this.work);
  this.work="";
  //document.querySelector("input")?.focus();
  this.element?.nativeElement.focus();
 }
 delete(index:number)
 {
this.todos.splice(index,1);
 }
 get(index:number){
  this.update=this.todos[index];
  this.indexUpdate=index;
  this.isUpdateFormActive=true

 }
 updateIn(){
this.todos[this.indexUpdate]=this.update
this.isUpdateFormActive=false
 }
}
