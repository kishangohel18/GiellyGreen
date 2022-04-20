import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { GGInvoiceService } from '../gginvoice.service';

@Component({
  selector: 'app-monthly-invoice',
  templateUrl: './monthly-invoice.component.html',
  styleUrls: ['./monthly-invoice.component.css']
})
export class MonthlyInvoiceComponent implements OnInit {

  date:any;
  month = null;
  invoiceRef:any;
  checked = false;
  indeterminate = false;
  setOfCheckedId = new Set<number>();
  allChecked = false; 
  Custom1 = "Custom 1";
  Custom2 = "Custom 2";
  Custom3 = "Custom 3";
  Custom4 = "Custom 4";
  Custom5  = "Custom 5";
  listOfData:any;
  editId: any;

  userSessionToken = sessionStorage.getItem("User");
  constructor(public router:Router,private fb: FormBuilder,private _gs:GGInvoiceService) { 

  }
  ngOnInit(): void {
    
  }
  onChange(result: Date): void {
    console.log('onChange: ', result);
    const chosenMonth = String(result.getMonth()+1);
    if(chosenMonth.length == 1){
      var chosenMonthString = "0"+ chosenMonth;
      console.log(chosenMonthString)
    }
    else{
      chosenMonthString = chosenMonth+"";
    }
    this.date = new Date();
    const curMonth = this.date.getUTCMonth()+1;
    const curYear = this.date.getUTCFullYear();
    this.invoiceRef = curMonth+"/"+curYear;
    this.getAllActiveSuppliers(chosenMonthString,result.getFullYear());
  }
  startEdit(id: any): void {
    this.editId = id;
  }

  stopEdit(): void {
    this.editId = null;
  }
  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfData.forEach((item: { id: number; }) => this.updateCheckedSet(item.id, value));
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: any): void {
    this.listOfData = $event;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfData.every((item: { id: number; }) => this.setOfCheckedId.has(item.id));
    this.indeterminate = this.listOfData.some((item: { id: number; }) => this.setOfCheckedId.has(item.id)) && !this.checked;
  }
  getAllActiveSuppliers(month:any,year:any){
    if (this.userSessionToken) {
      //const month  = this.month?.getUTCMonth()+1
      this._gs.getActiveSuppliers(this.userSessionToken,month,year).subscribe(
        (response: any) => {
          this.listOfData = response.Result
        }
      );
    }
  }
}
