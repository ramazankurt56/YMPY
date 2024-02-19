import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class VisiblePageService {

  constructor() { }
  visiblePageNumbers: number[] = [];
  generatePageNumbers(responseTotalPages:number,requestPageNumber:number,) {
      const totalPages = responseTotalPages;
      const currentPage = requestPageNumber;
      const maxPagesToShow = 6;
      let startPage: number, endPage: number;
      if (totalPages <= maxPagesToShow) {
          startPage = 1;
          endPage = totalPages;
      } else {
          if (currentPage <= Math.floor(maxPagesToShow / 2)) {
              startPage = 1;
              endPage = maxPagesToShow;
          } else if (currentPage + Math.floor(maxPagesToShow / 2) >= totalPages) {
              startPage = totalPages - maxPagesToShow + 1;
              endPage = totalPages;
          } else {
              startPage = currentPage - Math.floor(maxPagesToShow / 2);
              endPage = currentPage + Math.floor(maxPagesToShow / 2);
          }
      }
      this.visiblePageNumbers = Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
  }
  
}
