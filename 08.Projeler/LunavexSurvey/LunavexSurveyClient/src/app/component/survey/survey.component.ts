import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-survey',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './survey.component.html',
  styleUrl: './survey.component.css'
})
export class SurveyComponent {
  constructor() { this.selectedType = 'text'; }
  selectedType: string = "";
  @ViewChild('surveyRadio') surveyRadio: any;
  //@ViewChild('surveyRadio2') surveyRadio2: any;
  singleChoice: string = "";
  singleChoiceList: string[] = []; // Initialize with empty array
  id: number = 0
  myInput:any
  addRadio() {
    const container = this.surveyRadio.nativeElement;
    const originalElement = container.querySelector('.survey-radio');
    const clonedElement = originalElement.cloneNode(true);
    const deleteButton = document.createElement('button');
    deleteButton.textContent = 'Sil';
    deleteButton.className = 'btn btn-danger text';
    deleteButton.onclick = () => {
      container.removeChild(clonedElement);
    };
    clonedElement.appendChild(deleteButton);
    container.appendChild(clonedElement);
  }
  addChoice() {
    this.singleChoiceList=[]
    for (let index = 1; index < 3; index++) {
      var myDiv = document.getElementById(`survey-radio-${index}`);
      console.log(myDiv)
      this.myInput = myDiv?.querySelectorAll('input');
      for (var i = 0; i < this.myInput.length; i++) {
        this.singleChoiceList.push(this.myInput[i].value);
      }
  
    }
   
    //console.log(this.singleChoiceList)
  }
}
