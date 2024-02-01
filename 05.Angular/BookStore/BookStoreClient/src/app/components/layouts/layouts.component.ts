import { Component } from '@angular/core';
import { driver } from 'driver.js';
import "driver.js/dist/driver.css";
import { PopupService } from '../../services/popup.service';
@Component({
  selector: 'app-layouts',
  templateUrl: './layouts.component.html',
  styleUrl: './layouts.component.css'
})
export class LayoutsComponent {
  isPopupShow:boolean=false;
  constructor(
 public popup:PopupService 
  ) {} 

  showDriver(){
   
    const driverObj = driver({
      showProgress: true,
      steps: [
        { element: '#language', popover: { title: 'Language', description: 'Buradan dili değiştirebilirsiniz.' } },
        { element: '#categories', popover: { title: 'Categories', description: 'Bu kısımda kategorileri seçebilirsiniz.' } },
        { element: '#bookSearch', popover: { title: 'Book Search', description: 'Bu kısımda kitap arayabilirsiniz.' } },
        { element: '#book0', popover: { title: 'Book', description: 'Bu kısımda kitap detaylarını görüntüleyebilirsiniz.' } },
        { element: '#addShoppingCardBtn0', popover: { title: 'Add Shopping Cart', description: 'Bu kısımdan kitabı sepete ekleyebiliriz.' } },
        
        
      ]
    });
    driverObj.drive();
    this.popup.changePopupShow();
      }


}
