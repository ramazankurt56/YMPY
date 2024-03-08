import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm } from '@angular/forms';
import { Option, Question, SurveyModel } from '../../models/survey.model';
import { HttpService } from '../../services/http.service';
import { FormValidateDirective } from 'form-validate-angular';

@Component({
  selector: 'app-survey',
  standalone: true,
  imports: [FormsModule,FormValidateDirective],
  templateUrl: './survey.component.html',
  styleUrl: './survey.component.css'
})
export class SurveyComponent implements OnInit {
 
  createdQuestions : Question[] =  []
  createSurvey:SurveyModel=new SurveyModel();
  constructor(private formBuilder: FormBuilder,private http:HttpService) { 
 
   }
   productForm=new FormGroup({

   })

    ngOnInit(): void {
     // this.surveyForm = this.formBuilder.group({});
    }

  addNewQuestion()
  {
    this.createdQuestions.push({
      name : "",
      description: "",
      isRequired : false,
      type :0
    });
  }
  
  addNewOptionToQuestion(questionIndex : number){
    const question = this.createdQuestions[questionIndex];
    const optionToAdd : Option = {value:'' }
    question.choices = question.choices ? [ ...question.choices, optionToAdd ] : [ optionToAdd];
  }
  onSave()
  {
    console.log(this.createdQuestions);
  }
  saveSurvey(form: NgForm){
    if (form.valid) {
    this.createSurvey.createQuestionDto = this.createdQuestions.map(question => {
      question.type = parseInt(question.type.toString());
      return question;
    });
    console.log(this.createSurvey)
    this.http.post("Surveys/CreateSurvey", this.createSurvey, res => {
      //this.response = res;
    })
  }
  }
  // addRadio() {
  //   // const container = this.surveyRadio.nativeElement;
  //   // const originalElement = container.querySelector('.survey-radio');
  //   // const clonedElement = originalElement.cloneNode(true);
  //   // const deleteButton = document.createElement('button');
  //   // deleteButton.textContent = 'Sil';
  //   // deleteButton.className = 'btn btn-danger text';
  //   // deleteButton.onclick = () => {
  //   //   container.removeChild(clonedElement);
  //   // };
  //   // clonedElement.appendChild(deleteButton);
  //   // container.appendChild(clonedElement);
  // }
  // addChoice() {
  //   // this.singleChoiceList=[]
  //   // for (let index = 1; index < 3; index++) {
  //   //   var myDiv = document.getElementById(`survey-radio-${index}`);
  //   //   console.log(myDiv)
  //   //   this.myInput = myDiv?.querySelectorAll('input');
  //   //   for (var i = 0; i < this.myInput.length; i++) {
  //   //     this.singleChoiceList.push(this.myInput[i].value);
  //   //   }
  
  //   // }
   
  //   // //console.log(this.singleChoiceList)
  // }
}
