import { BookModel } from "./book.model"

export class PaymentModel{
     userId:number=0
        books:BookModel[]=[]
        buyer:BuyerModel=new BuyerModel();
        shippingAddress:AddressModel=new AddressModel();
        billingAddress:AddressModel=new AddressModel();
        paymentCard:PaymentCartModel=new PaymentCartModel();
}
export class BuyerModel
{
     id :string=""
     name :string="Ramazan"
     surname :string="Kurt"
     identityNumber :string="123456789"
     email :string="ramazan@gmail.com"
     gsmNumber :string="052505052"
     registrationDate :string=""
     lastLoginDate :string=""
     registrationAddress :string=""
     city :string=""
     country :string=""
     zipCode :string=""
     ip :string=""
}
export class AddressModel{
     description :string="İstasyon"
     zipCode :string="454545"
     contactName :string="Ramazan Kurt"
     city :string="İstanbul"
     country :string="Türkiye"
}
export class PaymentCartModel{
     cardHolderName :string="Ramazan Kurt"
     cardNumber :string=""
     expireYear :string=""
     expireMonth :string=""
     cvc :string="288"
}