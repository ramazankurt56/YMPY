import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
token:string=""
userId:number=0
userName:string=""

  constructor() { }
  isAuthentication(){
    const responeSting=localStorage.getItem("response")
    if(responeSting){
      const responseJson=JSON.parse(responeSting)
      this.token=responseJson.token
      this.userId=responseJson.userId
      this.userName=responseJson.userName
      return true
    }
    return false
  }
}
