import { Component, ElementRef, ViewChild } from '@angular/core';
import { PaginationResponseModel } from '../../../models/pagination-response.model';
import { DoctorModel } from '../../../models/doctor.model';
import { PaginationRequestModel } from '../../../models/pagination-request.model';
import { CommonModule } from '@angular/common';
import { HttpService } from '../../services/http.service';
import { FormsModule, NgForm } from '@angular/forms';
import { SwalService } from '../../services/swal.service';
import { FormValidateDirective } from 'form-validate-angular';
import { VisiblePageService } from '../../services/visible-page.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [CommonModule, FormsModule, FormValidateDirective],
  templateUrl: './doctor-list.component.html',
  styleUrl: './doctor-list.component.css'
})
export class DoctorListComponent {

  constructor(private router:Router,public visiblePage:VisiblePageService ,private http: HttpService, private swal: SwalService) { this.getAllDoctors() }

  response: PaginationResponseModel<DoctorModel[]> = new PaginationResponseModel<DoctorModel[]>()
  request: PaginationRequestModel = new PaginationRequestModel();
  addDoctorModel: DoctorModel = new DoctorModel();
  @ViewChild("addDoctorModalCloseBtn") addDoctorModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  changePage(pageNumber: number) {
    if (pageNumber < 1) {
      this.request.pageNumber = 1;
  } else {
      this.request.pageNumber = pageNumber;
      this.getAllDoctors();
  }
  }
  getAllDoctors() {
    this.http.post("Doctors/GetAll", this.request, (res) => {
      this.response = res;
      this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
    });
  }
  removeDoctor(doctorId:string) {
    this.http.get(`Doctors/DeleteById/${doctorId}`, (res) => {
      this.getAllDoctors();
      this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
      this.swal.callToast(res.message);    
    });
  }
  createDoctor(form: NgForm) {
    if (form.valid) {
      this.http.post("Doctors/Create", this.addDoctorModel, (res) => {
        this.addDoctorModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
        this.getAllDoctors();
      });
    }
  }
  getDoctorById(doctorId:any) {
    this.router.navigate(['/doctor-detail', doctorId]);
  }
  keyupSearch(){
    this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
    this.getAllDoctors();
  }
 

  clearInputInValidClass() {
    const inputs = document.querySelectorAll(".form-control.is-invalid");
    for (let i in inputs) {
      const el = inputs[i];
      el.classList.remove("is-invalid");
    }
  }

  clearAddDoctorModel() {
    this.addDoctorModel = new DoctorModel();
    this.clearInputInValidClass();
  }
}
