import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {
  surveyList:any=[]
  constructor(private swal: SwalService,private http:HttpService,private route: ActivatedRoute){this.getSurvey()}
 
  getSurvey(){
    this.http.get("Surveys/GetAllSurvey", res => {
      this.surveyList = res.data;
      console.log(this.surveyList)
    })
  }
  deleteSurvey(id:string){
    this.http.get("Surveys/DeleteSurvey/"+id,(res)=>{
      console.log(res)
      this.swal.callToast(res.data, "success");
      this.getSurvey()
    })
  }
}
