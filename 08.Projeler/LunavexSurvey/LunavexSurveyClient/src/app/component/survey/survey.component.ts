import { Component, OnInit } from '@angular/core';
import { CreateSurveyQuestion, Option, SurveyModel } from '../../models/survey.model';
import { HttpService } from '../../services/http.service';
import { FormsModule, NgForm } from '@angular/forms';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-survey',
  standalone: true,
  imports: [FormsModule,],
  templateUrl: './survey.component.html',
  styleUrl: './survey.component.css'
})
export class SurveyComponent implements OnInit{
  
  createdQuestions: CreateSurveyQuestion[] = []
  createSurvey: SurveyModel = new SurveyModel();
  constructor(private swal:SwalService,private http: HttpService) { 
  }
  ngOnInit(): void {
  }
  
  isValidQuestion(question: CreateSurveyQuestion): boolean {
    return this.isNameValid(question) &&
           this.isDescriptionValid(question) &&
           this.isOptionValueValid(question) ;
  }
  
  private isNameValid(question: CreateSurveyQuestion): boolean {
    return question.name.trim() !== '';
  }
  
  private isDescriptionValid(question: CreateSurveyQuestion): boolean {
    return  question.description?.trim() !== '';
  }
  
  private isOptionValueValid(question: CreateSurveyQuestion): boolean {
    if (question.choices) {
      return question.choices.every(option => option.value.trim() !== '');
    }
    return true;
  }
  
 
  addNewQuestion() {
    this.createdQuestions.push({
      name: "",
      description: "",
      isRequired: false,
      type: 0
    });
  }
  addNewOptionToQuestion(questionIndex: number) {
    const question = this.createdQuestions[questionIndex];
    const optionToAdd: Option = { value: '' }
    question.choices = question.choices ? [...question.choices, optionToAdd] : [optionToAdd];
  }

  saveSurvey(myForm:NgForm) {
    const allQuestionsValid = this.createdQuestions.every(question => this.isValidQuestion(question));
    if (myForm.valid){
    this.createSurvey.createQuestionDto = this.createdQuestions.map(question => {
      question.type = parseInt(question.type.toString());
      return question;
    });
    this.http.post("Surveys/CreateSurvey", this.createSurvey, res => {
      this.swal.callToast(res.data, "success");
    })
  }
  else {
    console.log('Form hatalı! Lütfen tüm soruları doldurun.');
  }
  
  }
  deleteQuestion(index: number) {
    const deleted = this.createdQuestions.splice(index, 1);
    this.swal.callToast("Question deleted", "success");
  }
  deleteChoice(index: number,questionIndex:number) {
    const deleted = this.createdQuestions[questionIndex].choices;
    deleted?.splice(index,1)
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
