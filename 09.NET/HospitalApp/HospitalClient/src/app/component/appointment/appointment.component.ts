import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { HttpService } from '../services/http.service';
import { SwalService } from '../services/swal.service';
import { AppointmentModel } from '../../models/appointment.model';

@Component({
  selector: 'app-appointment',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css'
})
export class AppointmentComponent {
  constructor(private datePipe: DatePipe,private http: HttpService, private swal: SwalService){ 
    this.startDate = this.getMonday(); this.calculateDays();
  }
  clocks: string[] = ["09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00"];
  startDate: Date=new Date;
  date: string[] = [];
  selectClock:string=""
  selectDate:string=""
  addAppointmentModel: AppointmentModel = new AppointmentModel();
  ileri() {
    this.updateStartDate(7);
  }
  
  geri() {
    this.updateStartDate(-7);
  }  
  updateStartDate(dayDifference: number) {
    const newStartDate = new Date(this.startDate);
    newStartDate.setDate(newStartDate.getDate() + dayDifference); // Set the new start date
    this.startDate = newStartDate; // Update the start date
    this.calculateDays();
  }
  calculateDays() {
    this.date=[]
    for (let i = 0; i < 5; i++) {
      const day = new Date(this.startDate);
      day.setDate(this.startDate.getDate() + i);
      const formattedDate = day.toLocaleDateString("tr-Tr", {
        year: "numeric",
        month: "numeric",
        day: "numeric",
      }).replace(/\./g, "-");

      this.date.push(formattedDate);
    }
    
  }
  getMonday(date: Date = new Date()) {
    const day = date.getDay();
    const diff = date.getDate() - day + (day === 0 ? -6 : 1); // If it's Sunday, move to Monday
    return new Date(date.setDate(diff));
    
  }
 createAppointment() {
  this.addAppointmentModel.patientId="d5288e5e-8ccd-4e31-a3ed-dcf01c3184c2"
  this.addAppointmentModel.doctorId="d69a07f3-9933-4c29-a893-e759c2def392"
  // Tarih string'ini ayrıştırma
let tarihParts: string[] = this.selectDate.replace(/(\d{2})-(\d{2})-(\d{4})/, "$3-$2-$1").split('-');
let year: number = parseInt(tarihParts[0]);
let month: number = parseInt(tarihParts[1]) - 1;//JavaScript'te aylar 0'dan başlar
let day: number = parseInt(tarihParts[2]);

// Saat string'ini ayrıştırma
let saatParts: string[] = this.selectClock.split(':');
let hour: number = parseInt(saatParts[0]);
let minute: number = parseInt(saatParts[1]);

// Yeni tarih ve saat nesnesi oluşturma
let appointmentDateTimes: Date = new Date(year, month, day, hour, minute);

// Sonucu kontrol etme
  this.addAppointmentModel.appointmentDateTime=appointmentDateTimes
  this.addAppointmentModel.notes="Önemli"
    this.http.post("Appointments/Create", this.addAppointmentModel, (res) => {
      this.swal.callToast(res.message);
    });
  }
  randevuAl( clock: string,date:string) {
    this.selectClock=clock
    this.selectDate=date
  }
}
