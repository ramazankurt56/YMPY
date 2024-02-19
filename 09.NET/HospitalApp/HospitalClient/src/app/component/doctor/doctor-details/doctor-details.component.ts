import { Component, OnInit } from '@angular/core';
import { VisiblePageService } from '../../services/visible-page.service';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { PaginationRequestModel } from '../../../models/pagination-request.model';
import { PaginationResponseModel } from '../../../models/pagination-response.model';
import { CommonModule } from '@angular/common';
import { PatientModel } from '../../../models/patient.model';
import { DoctorModel } from '../../../models/doctor.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-doctor-details',
  standalone: true,
  imports: [FormsModule,FormValidateDirective,CommonModule],
  templateUrl: './doctor-details.component.html',
  styleUrl: './doctor-details.component.css'
})
export class DoctorDetailsComponent implements OnInit {
  response: PaginationResponseModel<PatientModel[]> = new PaginationResponseModel<PatientModel[]>()
  request: PaginationRequestModel = new PaginationRequestModel();
  updateDoctorModel: DoctorModel = new DoctorModel();
  doctorId:string="";               
constructor(public visiblePage:VisiblePageService,private http: HttpService, private swal: SwalService,private route: ActivatedRoute )
{
  this.getAllPatients()
}
ngOnInit() {
  this.route.params.subscribe(params => {
    const doctorId = params['doctorId'];
    this.doctorId=doctorId
    this.getDoctorById()
  });
  
}

getAllPatients() {
  this.http.post("Patients/GetAll", this.request, (res) => {
    this.response = res;
    this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
  });
}
getDoctorById() {
  this.http.get(`Doctors/GetDoctorById/${this.doctorId}`, (res) => {
    this.updateDoctorModel.firstName=res.firstName
    this.updateDoctorModel.lastName=res.lastName
    this.updateDoctorModel.specialization=res.specialization
    this.updateDoctorModel.phoneNumber=res.phoneNumber
    this.updateDoctorModel.email=res.email
    this.updateDoctorModel.id=res.id
  });
}
updateDoctor(form: NgForm) {
  if (form.valid) {
    this.http.post("Doctors/Update", this.updateDoctorModel, (res) => {
      this.swal.callToast(res.message);
      //this.getAllDoctors();
    });
  }
}
changePage(pageNumber: number) {
  if (pageNumber < 1) {
    this.request.pageNumber = 1;
} else {
    this.request.pageNumber = pageNumber;
   // this.getAllDoctors();
}
}
}
