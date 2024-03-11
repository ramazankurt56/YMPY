export class SurveySubmissionModel{
    surveyId:string=""
    CreateQuestionValueDtos:SurveyQuestionValue[]=[]
    }   
    
    export interface SurveyQuestionValue{
        questionId: string,
        SurveySubmissionId:string
        value : string,
    }
    
export class GetSurveySubmissionModel{
    id:string=""
    name:string=""
    description:string="";
    questions:SurveySubmissionQuestion[]=[]
    }   
    
    export interface SurveySubmissionQuestion{
        id:string,
        name: string,
        surveyId:string
        description : string | null,
        type: number,
        isRequired :boolean
        choices?: Option[],
        value:string
    }
    export interface Option{
        value : string
    }
    