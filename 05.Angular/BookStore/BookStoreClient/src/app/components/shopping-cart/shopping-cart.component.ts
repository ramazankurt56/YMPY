import { Component } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { TranslateService } from '@ngx-translate/core';
import { PaymentModel } from '../../models/payment.model';
import { Cities, Countries } from '../../constants/address';
import { SwalService } from '../../services/swal.service';
import { AuthService } from '../../services/auth.service';
import { ErrorService } from '../../services/error.service';


@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css'
  
})
export class ShoppingCartComponent {
  [key:string]:any
language:string="en"
selectedTab:number=1
request:PaymentModel=new PaymentModel();
countries=Countries;
cities=Cities;
isSameAddress:boolean=false
cardNumber1:string="5528"
cardNumber2:string="7900"
cardNumber3:string="0000"
cardNumber4:string="0008"
expireMonthAndYear:string="2024-05"
selectedCurrencyForPaymnent:string="₺"
constructor(public shopping:ShoppingCartService,private translate:TranslateService, private swal:SwalService ,private auth:AuthService) {
if(localStorage.getItem("language"))
{
  this.language=localStorage.getItem("language") as string;
}
translate.setDefaultLang(this.language)
this.request.books=shopping.shoppingCarts;
shopping.calcTotal()
}
changeTab(tabNumber:number){
  this.selectedTab=tabNumber
}
setSelectedPaymentCurrency(currency:string){
this.selectedCurrencyForPaymnent=currency
const newBooks=this.shopping.shoppingCarts.filter(p=>p.price.currency===this.selectedCurrencyForPaymnent)
this.request.books=newBooks;
}
payment(){
  this.request.paymentCard.expireMonth=this.expireMonthAndYear.substring(5);
  this.request.paymentCard.expireYear=this.expireMonthAndYear.substring(0,4);
  this.request.paymentCard.cardNumber=this.cardNumber1+this.cardNumber2+this.cardNumber3+this.cardNumber4
  this.request.buyer.registrationAddress=this.request.billingAddress.description
  this.request.buyer.city=this.request.billingAddress.city
  this.request.buyer.country=this.request.billingAddress.country
  this.request.userId=this.auth.userId
  this.shopping.payment(this.request,(res)=>{
const btn=document.getElementById("paymentModalCloseBtn")
btn?.click();
const remainShoppingCarts=this.shopping.shoppingCarts.filter(p=>p.price.currency!==this.selectedCurrencyForPaymnent)
localStorage.setItem("shoppingCarts",JSON.stringify(remainShoppingCarts))
this.shopping.getAllShoppingCarts();
this.translate.get("paymentIsSuccessful").subscribe(translate=>{
  this.swal.callToast(translate,"success")
})
  })
}
changeIsSameAddress(){
if(this.isSameAddress)
{
  this.request.billingAddress={...this.request.shippingAddress}
}
}

gotoNextInputIf4Lenght(inputCount:string="",value:string="")
{
  this[`cardNumber${inputCount}`]=value.replace(/[^0-9]/g,"");
  value=value.replace(/[^0-9]/g,"");
if(value.length===4)
{
  if(inputCount==="4")
  {
    const el= document.getElementById("expireMonthAndYear")
    el?.focus();
  }
  else{
    const id:string=`cartNumber${+inputCount+1}`
    const el:HTMLInputElement=document.getElementById(id) as HTMLInputElement;
    el.focus();
  }
}
}
setExpireMonthAndYear(){
  //Bu kısmı input mount tipi seçtiğimiz için iptal ettik
  this.expireMonthAndYear = this.expireMonthAndYear.replace(/[^0-9]/g, "");

  if(this.expireMonthAndYear.length>2){
    this.expireMonthAndYear=this.expireMonthAndYear.substring(0,2)+"/"+this.expireMonthAndYear.substring(2)
  }

  if(this.expireMonthAndYear.length>=2)
  {
    const month=parseInt(this.expireMonthAndYear.substring(0,2));
    if(month===0){
      this.expireMonthAndYear="01"+this.expireMonthAndYear.substring(2);
    }else if(month>12){
      this.expireMonthAndYear="12"+this.expireMonthAndYear.substring(2)
    }
  }
  if(this.expireMonthAndYear.length>4){
    const el =document.getElementById("cvc")
    el?.focus()
  }
}
}
