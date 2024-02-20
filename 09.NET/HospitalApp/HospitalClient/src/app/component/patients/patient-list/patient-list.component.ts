import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { VisiblePageService } from '../../services/visible-page.service';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { PatientModel } from '../../../models/patient.model';
import { PaginationResponseModel } from '../../../models/pagination-response.model';
import { PaginationRequestModel } from '../../../models/pagination-request.model';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule, FormsModule, FormValidateDirective],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css'
})
export class PatientListComponent {

  constructor(private router:Router,public visiblePage:VisiblePageService ,private http: HttpService, private swal: SwalService) { this.getAllPatients() }

  response: PaginationResponseModel<PatientModel[]> = new PaginationResponseModel<PatientModel[]>()
  request: PaginationRequestModel = new PaginationRequestModel();
  addPatientModel: PatientModel = new PatientModel();
  @ViewChild("addPatientModalCloseBtn") addPatientModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  changePage(pageNumber: number) {
    if (pageNumber < 1) {
      this.request.pageNumber = 1;
  } else {
      this.request.pageNumber = pageNumber;
      this.getAllPatients();
  }
  }
  getAllPatients() {
    this.http.post("Patients/GetAll", this.request, (res) => {
      this.response = res;
      this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
    });
  }
  removePatient(patientId:string) {
    this.http.get(`Patients/DeleteById/${patientId}`, (res) => {
      this.getAllPatients();
      this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
      this.swal.callToast(res.message);    
    });
  }
  createPatient(form: NgForm) {
    if (form.valid) {
      console.log(form)
     console.log( this.addPatientModel.dateOfBirth)
      this.http.post("Patients/Create", this.addPatientModel, (res) => {
        this.addPatientModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
        this.getAllPatients();
      });
    }
  }
  getPatientById(patientId:any) {
    this.router.navigate(['/patient-detail', patientId]);
  }
  keyupSearch(){
    this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
    this.getAllPatients();
  }
 

  clearInputInValidClass() {
    const inputs = document.querySelectorAll(".form-control.is-invalid");
    for (let i in inputs) {
      const el = inputs[i];
      el.classList.remove("is-invalid");
    }
  }

  clearAddPatientModel() {
    this.addPatientModel = new PatientModel();
    this.clearInputInValidClass();
  }
}
