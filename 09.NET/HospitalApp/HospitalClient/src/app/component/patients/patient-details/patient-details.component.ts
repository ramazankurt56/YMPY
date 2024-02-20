import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { PaginationResponseModel } from '../../../models/pagination-response.model';
import { PatientModel } from '../../../models/patient.model';
import { PaginationRequestModel } from '../../../models/pagination-request.model';
import { DoctorModel } from '../../../models/doctor.model';
import { VisiblePageService } from '../../services/visible-page.service';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-patient-details',
  standalone: true,
  imports: [FormsModule,FormValidateDirective,CommonModule],
  templateUrl: './patient-details.component.html',
  styleUrl: './patient-details.component.css'
})
export class PatientDetailsComponent implements OnInit {
  response: PaginationResponseModel<DoctorModel[]> = new PaginationResponseModel<DoctorModel[]>()
  request: PaginationRequestModel = new PaginationRequestModel();
  updatePatientModel: PatientModel = new PatientModel();
  patientId:string="";               
  doctorId:string="";
constructor(private router:Router,private datePipe: DatePipe,public visiblePage:VisiblePageService,private http: HttpService, private swal: SwalService,private route: ActivatedRoute )
{
  this.getAllDoctors()
}
ngOnInit() {
  this.route.params.subscribe(params => {
    const patientId = params['patientId'];
    this.patientId=patientId
    this.getPatientById()
  });
  
}
getDoctorById(doctorId:string) {
  this.router.navigate(['/doctor-detail', doctorId]);
}
getAllDoctors() {
  this.http.post("Doctors/GetAll", this.request, (res) => {
    this.response = res;
    this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
  });
}
getPatientById() {
  this.http.get(`Patients/GetPatientById/${this.patientId}`, (res) => {
    this.updatePatientModel.identificationNumber=res.identificationNumber
    this.updatePatientModel.firstName=res.firstName
    this.updatePatientModel.lastName=res.lastName
    this.updatePatientModel.gender=res.gender
    this.updatePatientModel.phoneNumber=res.phoneNumber
    this.updatePatientModel.address=res.address
    this.updatePatientModel.dateOfBirth=this.datePipe.transform(res.dateOfBirth, 'yyyy-MM-dd') || ''
    console.log(this.updatePatientModel.dateOfBirth)
    this.updatePatientModel.id=res.id
  });
}
updatePatient(form: NgForm) {
  if (form.valid) {
    this.http.post("Patients/Update", this.updatePatientModel, (res) => {
      this.swal.callToast(res.message);
    });
  }
}
changePage(pageNumber: number) {
  if (pageNumber < 1) {
    this.request.pageNumber = 1;
} else {
    this.request.pageNumber = pageNumber;
}
}
}
