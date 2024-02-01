import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PopupService {
  processBar: number = 0;
  isPopupShow: boolean = false;
  notShowThisPopup: boolean = false;


  constructor() {
    if (localStorage.getItem("notShowDiscoverPopupAgain")) {
      this.notShowThisPopup = true
    }
  }
  showDriverPopup() {
    if (!this.notShowThisPopup) {
      setTimeout(() => {
        this.changePopupShow();
      }, 2000);
      setTimeout(() => {
        if (this.isPopupShow) {
          this.changePopupShow();
        }
      }, 8000);
    }
  }

  changePopupShow() {
    this.isPopupShow = !this.isPopupShow
  }
  notShowAgain() {
    localStorage.setItem("notShowDiscoverPopupAgain", "true");
    this.notShowThisPopup = true;
  }
}

