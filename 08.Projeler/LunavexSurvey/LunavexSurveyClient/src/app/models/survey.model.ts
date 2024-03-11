export class SurveyModel{
name:string=""
description:string="";
createQuestionDto:CreateSurveyQuestion[]=[]

}   
export interface CreateSurveyQuestion{
    name: string,
    description : string | null,
    type: number,
    isRequired :boolean
    choices?: Option[]
}
export interface Option{
    value : string,
}
