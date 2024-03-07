export class LoginModel{

}   


export interface Question{
    name: string,
    description : string | null,
    questionType: "text" | "time" | string,
    required :boolean
}