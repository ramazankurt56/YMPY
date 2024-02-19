import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LayoutComponent } from './component/layout/layout.component';
import { HomeComponent } from './component/home/home.component';
import { AppointmentComponent } from './component/appointment/appointment.component';
import { ExaminationComponent } from './component/examination/examination.component';
import { MedicationComponent } from './component/medication/medication.component';
import { PrescriptionComponent } from './component/prescription/prescription.component';
import { DoctorListComponent } from './component/doctor/doctor-list/doctor-list.component';
import { DoctorDetailsComponent } from './component/doctor/doctor-details/doctor-details.component';
import { PatientListComponent } from './component/patients/patient-list/patient-list.component';
import { PatientDetailsComponent } from './component/patients/patient-details/patient-details.component';

export const routes: Routes = [
    {
        path:"login",
        component:LoginComponent
    },
    {
        path:"",
        component:LayoutComponent,
        //canActivateChild: [()=> inject(AuthService).isAuthenticated()],
        children:[
            {
                path:"",
                component:HomeComponent
            },
            {
                path:"doctor-list",
                component:DoctorListComponent,
               
            },
            {
                path:'doctor-detail/:doctorId',
                component:DoctorDetailsComponent,
               
            },
            {
                path:"patient-list",
                component:PatientListComponent
            },
            {
                path:'patient-detail/:patientId',
                component:PatientDetailsComponent
            }
            ,
            {
                path:"appointment",
                component:AppointmentComponent
            },
            {
                path:"examination",
                component:ExaminationComponent
            },
            {
                path:"medication",
                component:MedicationComponent
            },
            {
                path:"prescription",
                component:PrescriptionComponent
            }
        ]
    }
];
