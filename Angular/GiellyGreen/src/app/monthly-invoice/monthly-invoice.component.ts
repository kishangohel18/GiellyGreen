import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-monthly-invoice',
  templateUrl: './monthly-invoice.component.html',
  styleUrls: ['./monthly-invoice.component.css']
})
export class MonthlyInvoiceComponent implements OnInit {

  date = null;
  userSessionToken = sessionStorage.getItem("User");
  constructor(public router:Router) { }

  ngOnInit(): void {
    if(!this.userSessionToken){
      this.router.navigate(['/login'])
    }
  }
  onChange(result: Date): void {
    console.log('onChange: ', result);
  }

}
