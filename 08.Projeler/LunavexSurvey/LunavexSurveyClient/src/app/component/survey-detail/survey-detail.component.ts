import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { FormsModule, NgForm } from '@angular/forms';
import {  UpdateSurveyModel,Option, UpdateSurveyQuestion } from '../../models/update-survey.model';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-survey-detail',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './survey-detail.component.html',
  styleUrl: './survey-detail.component.css'
})
export class SurveyDetailComponent implements OnInit {
  surveyId:string=""
  constructor(private swal:SwalService,private http:HttpService,private router: ActivatedRoute){}
  getAllQuestion : UpdateSurveyQuestion[] =  []
  getAllSurvey:UpdateSurveyModel=new UpdateSurveyModel();
  updateSurvey:UpdateSurveyModel=new UpdateSurveyModel();
  ngOnInit(): void {
    this.router.params.subscribe(params => {
      this.surveyId = params['id'];
      console.log(this.surveyId)
      this.getSurvey();
    });
  }
  addNewQuestion()
  {
    this.getAllQuestion.push({
      surveyId:"",
      name : "",
      description: "",
      isRequired : false,
      type :0,
      isDeleted:false
    });
  }
  addNewOptionToQuestion(questionIndex : number){
    const question = this.getAllQuestion[questionIndex];
    const optionToAdd : Option = {value:'',isDeleted:false }
    question.choices = question.choices ? [ ...question.choices, optionToAdd ] : [ optionToAdd];
  }
  updateSurveys(myForm: NgForm){
    if (myForm.valid){
    this.updateSurvey.name=this.getAllSurvey.name
    this.updateSurvey.description=this.getAllSurvey.description
    this.updateSurvey.id=this.surveyId 
    this.updateSurvey.updateQuestionDto = this.getAllQuestion.map(question => {
      question.type = parseInt(question.type.toString());
      return question;
    });
    this.http.post("Surveys/UpdateSurvey", this.updateSurvey, res => {
      this.swal.callToast(res.data, "success");
      this.getSurvey()
    })
  }
    else {
      console.log('Form hatalı! Lütfen tüm soruları doldurun.');
    }
  }
  getSurvey(){
    this.http.get("Surveys/GetBySurveyId/"+this.surveyId, res => {
      this.getAllSurvey = res.data;
      this.getAllQuestion=res.data.questions
    })
  }
  deleteQuestion(index:number){
    const deleted = this.getAllQuestion[index]
    deleted.isDeleted=true
    this.swal.callToast("Question deleted", "success");
  }
  deleteChoice(index: number, questionIndex: number) {
    const question = this.getAllQuestion[questionIndex];
    if (question && question.choices && question.choices[index]) {
        question.choices[index].isDeleted = true;
        this.swal.callToast("Choice deleted", "success");
    } else {
        console.error("Hata: Belirtilen soru veya seçenek bulunamadı.");
    }
}
}
