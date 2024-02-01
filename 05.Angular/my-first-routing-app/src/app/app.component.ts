import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template:`
  <ul>
    <li routerLink="/">Home</li>
    <li routerLink="/about">About</li>
    <li routerLink="/contact">Contact</li>
  </ul>
  <router-outlet></router-outlet>
  `
})
export class AppComponent {
  title = 'my-first-routing-app';
}
