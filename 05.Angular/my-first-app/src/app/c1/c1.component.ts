import { Component, OnDestroy, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-c1',
  templateUrl: './c1.component.html',
  styleUrl: './c1.component.css'
})
export class C1Component implements OnInit,OnDestroy{
  data: string="";
  private subscription: Subscription=new Subscription;
  constructor(private dataService: DataService) {}
  
  ngOnInit() {
    // Servisten gelen veriyi dinliyoruz
    this.subscription = this.dataService.getData().subscribe((data) => {
      this.data = data;
    });
  }

  ngOnDestroy() {
    // Aboneliği iptal ediyoruz
    this.subscription.unsubscribe();
  }

  changeData() {
    // Veriyi değiştiriyoruz ve servise gönderiyoruz
    const newData = 'Merhaba Dünya!';
    this.dataService.setData(newData);
  }
}
