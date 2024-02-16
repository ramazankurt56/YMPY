import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestModel } from '../../models/request.model';
import { BookModel } from '../../models/book.model';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { SwalService } from '../../services/swal.service';
import { TranslateService } from '@ngx-translate/core';
import { addShoppingCartModel } from '../../models/add-shopping-cart.model';
import { AuthService } from '../../services/auth.service';
import { ErrorService } from '../../services/error.service';
import { PopupService } from '../../services/popup.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  books: BookModel[] = [];
  pageNumbers: number[] = [];
  categories: any = []
  searchCategory: string = "";
  request: RequestModel = new RequestModel();
  newData: any[] = []
  loaderDatas=[1,2,3,4,5,6]
  isLoading:boolean=false;
  //https://localhost:7215/api/Books/GetAll/1/10
  //https://localhost:7215/api/Books/GetAll

  constructor(private http: HttpClient, 
    private shopping: ShoppingCartService,
    private swal:SwalService,
    private translate:TranslateService,
    private auth:AuthService,
    private error:ErrorService,
    private popup:PopupService
    ) {
      if(localStorage.getItem("request")){
        const requestString:any=localStorage.getItem("request");
        const requestObj=JSON.parse(requestString)
        this.request=requestObj
      }
    this.categoriesGetAll();
    
  }

  addShoppingCart(book: BookModel) {
    if(localStorage.getItem("response")){
      const data:addShoppingCartModel=new addShoppingCartModel();
      data.bookId=book.id
      data.price=book.price
      data.quantity=1
      data.userId=this.auth.userId;
      this.http.post("https://localhost:7215/api/ShoppingCarts/Add/",data).subscribe({
        next:(res:any)=>{
        
            this.shopping.getAllShoppingCarts();
            this.translate.get("addBookInShoopingCartIsSuccessful").subscribe(res=>{
              this.swal.callToast(res)
            })
        },
        error:(err:HttpErrorResponse)=>{
             this.error.errorHandler(err);
        }
    })
    }
    else{
      if(book.quantity<1){
        this.translate.get("bookQuantityIsNotEnough").subscribe(res=>{
          this.swal.callToast(res,"error")
        })
      }else{
        const checkBookIsAlreadyExists=this.shopping.shoppingCarts.filter(p=>p.id==book.id)[0]
        if(checkBookIsAlreadyExists!==undefined)
        {
          this.shopping.shoppingCarts.filter(p=>p.id==book.id)[0].quantity+=1
        }
        else{
          const newBook={...book}
          newBook.quantity
          this.shopping.shoppingCarts.push(newBook)
        }
        
        this.shopping.calcTotal()
        localStorage.setItem("shoppingCarts", JSON.stringify(this.shopping.shoppingCarts))
        this.translate.get("addBookInShoopingCartIsSuccessful").subscribe(res=>{
          this.swal.callToast(res)
        });
      }   
     // this.shopping.count++;
    }
  }
  feedData() {
    this.request.pageSize += 10;
    this.newData = [];
    this.getAll();
  }

  changeCategory(categoryId: number | null = null) {
    this.request.categoryId = categoryId;
    this.request.pageSize = 0;
    this.searchCategory = "";
    this.feedData()
  }

  getAll() {
    this.isLoading=true
    this.http.post<BookModel[]>(`https://localhost:7215/api/Books/GetAll/`, this.request)
      .subscribe({next:(res:any)=>{
          this.books = res
          this.isLoading=false
          localStorage.setItem("request",JSON.stringify(this.request));
          console.log(res)
      },
      error:(err:HttpErrorResponse)=>{
        this.error.errorHandler(err);
      }
    })
  }
  categoriesGetAll() {
    this.isLoading=true
    this.http.get(`https://localhost:7215/api/Categories/GetAll`)
      .subscribe({
        next:(res:any)=>{
          this.categories = res;
          this.getAll();
          this.isLoading=false
          this.popup.showDriverPopup()
        },
        error:(err:HttpErrorResponse)=>{
          this.error.errorHandler(err);
        }
      })
  }

}
