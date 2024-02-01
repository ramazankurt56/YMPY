import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [MenubarModule,CommonModule,RouterOutlet,InputTextModule,ButtonModule],
  templateUrl: './layouts.component.html',
  styleUrl: './layouts.component.css'
})
export class LayoutsComponent implements OnInit {
  items: MenuItem[] | undefined;

constructor(private router:Router) {
  
}
  ngOnInit() {
      this.items = [
          {
              label: 'Anasayfa',
              icon: 'pi pi-fw pi-home',
          }
      ];
  }
  logout(){
this.router.navigateByUrl("/login")
  }
}
