export class SurveyModel{
name:string=""
description:string="";
createQuestionDto:Question[]=[]
}   


export interface Question{
    name: string,
    description : string | null,
    type: number,
    isRequired :boolean
    choices?: Option[]
}
export interface Option{
    value : string,
   // isTrueOption : boolean
}
