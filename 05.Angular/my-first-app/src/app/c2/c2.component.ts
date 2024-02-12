import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-c2',
  templateUrl: './c2.component.html',
  styleUrl: './c2.component.css'
})
export class C2Component implements OnInit {
  data: string="";

  constructor(private dataService: DataService) { }
  ngOnInit() {
     // Servisten gelen veriyi dinliyoruz
     this.dataService.getData().subscribe((data) => {
      this.data = data;
      console.log(data);
      console.log(this.data);
    });
  }

}
