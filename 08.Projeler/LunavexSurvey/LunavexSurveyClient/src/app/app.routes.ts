import { Routes } from '@angular/router';
import { LayoutComponent } from './component/layout/layout.component';
import { HomeComponent } from './component/home/home.component';
import { SurveyComponent } from './component/survey/survey.component';
import { SurveysubmissionComponent } from './component/surveysubmission/surveysubmission.component';
import { SurveyDetailComponent } from './component/survey-detail/survey-detail.component';

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
                path:"survey-detail/:id",
                component:SurveyDetailComponent
            },
            {
            path:"survey-submission/:id",
            component:SurveysubmissionComponent
            },
    ]
    }
];
