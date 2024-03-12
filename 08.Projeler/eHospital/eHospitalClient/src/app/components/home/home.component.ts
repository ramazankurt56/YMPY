import { Component, ElementRef, OnInit, ViewChild, ViewChildren, viewChild } from '@angular/core';
import { UserModel } from '../../models/user.model';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { DxSchedulerModule } from 'devextreme-angular';
import { HttpService } from '../../services/http.service';
import { AppointmentModel } from '../../models/create-appointment.model';
import { createApplication } from '@angular/platform-browser';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ DxSchedulerModule,  
    FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  appointmentsData: any[] = [];

  selectedDoctorId: string = "";

  currentDate: Date = new Date();

  doctors: UserModel[] = [];

  constructor(private http: HttpService,private swal:SwalService) { }
  appointmentCreate:AppointmentModel=new AppointmentModel()
  ngOnInit(): void {
    this.getAllDoctors();
  }
  @ViewChild("addStudentModalCloseBtn") addStudentModalCloseBtn: ElementRef<HTMLButtonElement> | undefined; 
  onAppointmentDblClick(e:any){
    console.log("test")
    e.cancel = true;
  this.addStudentModalCloseBtn?.nativeElement.click();  
}
onClick(e:any){
    console.log(e.appointmentData)
    this.appointmentCreate.doctorId=this.selectedDoctorId
    this.appointmentCreate.identityNumber=e.appointmentData.identityNumber
    this.appointmentCreate.startDate=e.appointmentData.startDate
    this.appointmentCreate.endDate=e.appointmentData.endDate
    this.http.post("Appointments/Create", this.appointmentCreate, res => {
      this.swal.callToast(res.data, "success");
      this.getDoctorAppointments() 
    })
    
 
}

  getAllDoctors() {
    this.http.get("Doctors/GetAllDoctors", res => {
      this.doctors = res.data;
    });
  }
  
 
  getDoctorAppointments() {
    if (this.selectedDoctorId === "") return;

    this.http.get("Appointments/GetAllByDoctorId/"+this.selectedDoctorId, res=>{
      console.log(res.data);
    
      const data = res.data.map((val: any, i: number) => {
        return {
          text: val.patient.fullName,
          startDate: new Date(val.startDate),
          endDate: new Date(val.endDate)
        };
    })

      this.appointmentsData = data;
    })
  }
}
