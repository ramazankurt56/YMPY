import { CommonModule, DatePipe } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { VisiblePageService } from '../services/visible-page.service';
import { HttpService } from '../services/http.service';
import { SwalService } from '../services/swal.service';
import { PaginationResponseModel } from '../../models/pagination-response.model';
import { MedicationModel } from '../../models/medication.model';
import { PaginationRequestModel } from '../../models/pagination-request.model';

@Component({
  selector: 'app-medication',
  standalone: true,
  imports: [CommonModule, FormsModule, FormValidateDirective],
  templateUrl: './medication.component.html',
  styleUrl: './medication.component.css'
})
export class MedicationComponent {

  constructor(private datePipe: DatePipe,public visiblePage:VisiblePageService ,private http: HttpService, private swal: SwalService) { this.getAllMedications() }

  response: PaginationResponseModel<MedicationModel[]> = new PaginationResponseModel<[MedicationModel]>()
  request: PaginationRequestModel = new PaginationRequestModel();
  addMedicationModel: MedicationModel = new MedicationModel();
  updateMedicationModel: MedicationModel = new MedicationModel();
  @ViewChild("addMedicationModalCloseBtn") addMedicationModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateMedicationModalCloseBtn") updateMedicationModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  changePage(pageNumber: number) {
    if (pageNumber < 1) {
      this.request.pageNumber = 1;
  } else {
      this.request.pageNumber = pageNumber;
      this.getAllMedications();
  }
  }
  getAllMedications() {
    this.http.post("Medications/GetAll", this.request, (res) => {
      this.response = res;
      this.visiblePage.generatePageNumbers(this.response.totalPages,this.request.pageNumber);
    });
  }
  getMedicationById(medicationId:any) {
    this.http.get(`Medications/GetMedicationById/${medicationId}`, (res) => {
      this.updateMedicationModel.medicationName=res.medicationName
      this.updateMedicationModel.quantity=res.quantity
      this.updateMedicationModel.expiryDate=this.datePipe.transform(res.expiryDate, 'yyyy-MM-dd') || ''
      this.updateMedicationModel.id=res.id
      console.log(this.updateMedicationModel)
    });
  }
  removeMedication(medicationId:string) {
    this.http.get(`Medications/DeleteById/${medicationId}`, (res) => {
      this.getAllMedications();
      this.swal.callToast(res.message);    
    });
  }
  updateMedication(form: NgForm) {
    if (form.valid) {
      this.http.post("Medications/Update", this.updateMedicationModel, (res) => {
        this.updateMedicationModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
        this.getAllMedications();
      });
    }
  }
  createMedication(form: NgForm) {
    if (form.valid) {
      this.http.post("Medications/Create", this.addMedicationModel, (res) => {
        this.addMedicationModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
        this.getAllMedications();
      });
    }
  }
  keyupSearch(){
    this.getAllMedications();
  }
 

  clearInputInValidClass() {
    const inputs = document.querySelectorAll(".form-control.is-invalid");
    for (let i in inputs) {
      const el = inputs[i];
      el.classList.remove("is-invalid");
    }
  }

  clearAddMedicationModel() {
    this.addMedicationModel = new MedicationModel();
    this.clearInputInValidClass();
  }
  clearUpdateMedicationModel() {
    this.updateMedicationModel = new MedicationModel();
    this.clearInputInValidClass();
  }
}
