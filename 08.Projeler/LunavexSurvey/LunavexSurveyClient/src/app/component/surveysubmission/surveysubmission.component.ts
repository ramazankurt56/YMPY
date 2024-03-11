import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { ActivatedRoute } from '@angular/router';
import { GetSurveySubmissionModel, SurveyQuestionValue, SurveySubmissionModel, SurveySubmissionQuestion } from '../../models/survey-submission.model';
import { FormsModule, NgForm } from '@angular/forms';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-surveysubmission',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './surveysubmission.component.html',
  styleUrl: './surveysubmission.component.css'
})
export class SurveysubmissionComponent implements OnInit {
  surveyId: string = ""
  getAllSurvey: GetSurveySubmissionModel = new GetSurveySubmissionModel();
  CreateSurveySubmission: SurveySubmissionModel = new SurveySubmissionModel()
  CreateQuestionValue: SurveyQuestionValue[] = []

  constructor(private http: HttpService, private router: ActivatedRoute,private swal:SwalService) { }
  ngOnInit(): void {
    this.router.params.subscribe(params => {
      this.surveyId = params['id'];
      console.log(this.surveyId)
      this.getSurvey();
    });
  }
  saveSurveySubmission(myForm: NgForm) {
    if (myForm.valid) {
    this.CreateSurveySubmission.surveyId = this.surveyId
    for (let index = 0; index < this.getAllSurvey.questions.length; index++) {
      this.CreateQuestionValue.push({
        questionId: this.getAllSurvey.questions[index].id,
        SurveySubmissionId: "",
        value: this.getAllSurvey.questions[index].value
      });
    }
    this.CreateSurveySubmission.CreateQuestionValueDtos = this.CreateQuestionValue
    console.log(this.CreateSurveySubmission)
    this.http.post("SurveySubmissions/CreateSurveySubmission", this.CreateSurveySubmission, res => {
      this.swal.callToast(res.data, "success");
      this.getSurvey()
      this.CreateQuestionValue=[]
    })
    } else {
    }
  }
  getSurvey() {
    this.http.get("Surveys/GetBySurveyId/" + this.surveyId, res => {
      this.getAllSurvey = res.data;
    });
  }
}
