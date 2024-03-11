export class UpdateSurveyModel{
    id:string=""
    name:string=""
    description:string="";
    updateQuestionDto:UpdateSurveyQuestion[]=[]
    }   
    
    export interface UpdateSurveyQuestion{
        name: string,
        surveyId:string
        description : string | null,
        type: number,
        isRequired :boolean
        choices?: Option[],
        isDeleted:boolean
    }
    export interface Option{
        value : string,
        isDeleted:boolean
    }
    