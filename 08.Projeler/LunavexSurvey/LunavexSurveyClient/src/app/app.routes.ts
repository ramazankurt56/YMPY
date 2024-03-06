import { Routes } from '@angular/router';
import { LayoutComponent } from './component/layout/layout.component';
import { HomeComponent } from './component/home/home.component';
import { SurveyComponent } from './component/survey/survey.component';
import { SurveysubmissionComponent } from './component/surveysubmission/surveysubmission.component';

export const routes: Routes = [
    {
        path:"",
        component:LayoutComponent,
        children:[
            {
            path:"",
            component:HomeComponent
            },
            {
            path:"survey",
            component:SurveyComponent
            },
            {
            path:"survey-submission",
            component:SurveysubmissionComponent
            },
    ]
    }
];
