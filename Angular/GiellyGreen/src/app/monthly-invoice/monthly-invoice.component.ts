import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { GGInvoiceService } from '../gginvoice.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-monthly-invoice',
  templateUrl: './monthly-invoice.component.html',
  styleUrls: ['./monthly-invoice.component.css']
})
export class MonthlyInvoiceComponent implements OnInit {

  //#region properties
  date: any;
  month = null;
  invoiceRef: any;
  checked = false;
  indeterminate = false;
  setOfCheckedId = new Set<number>();
  allChecked = false;
  Custom1: any;
  Custom2: any;
  Custom3: any;
  Custom4: any;
  Custom5: any;
  listOfData: any;
  currentHeaderId: any;
  chosenMonthString: any;
  chosenYear: any
  editId: any;
  arrayOfCheckedId: any;
  loading = true;
  checkedApproved = false;

  userSessionToken = sessionStorage.getItem("User");
  constructor(public router: Router, private fb: FormBuilder, private _gs: GGInvoiceService) {

  }
  ngOnInit(): void {

  }
  onChange(result: Date): void {
    const chosenMonth = String(result.getMonth() + 1);
    if (chosenMonth.length == 1) {
      this.chosenMonthString = "0" + chosenMonth;
    }
    else {
      this.chosenMonthString = chosenMonth + "";
    }
    this.date = new Date();
    const curYear = this.date.getUTCFullYear();
    this.invoiceRef = this.date.toLocaleString('en-us', { month: 'long' }) + "" + curYear;
    this.chosenYear = result.getFullYear();
    this.getCustomHeaders(this.chosenMonthString, this.chosenYear);
    this.getAllActiveSuppliers(this.chosenMonthString, this.chosenYear);
  }

  //for editable cells
  startEdit(id: any): void {
    this.editId = id;
  }

  stopEdit(): void {
    this.editId = null;
  }

  //to get all checked sets
  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
    this.arrayOfCheckedId = Array.from(this.setOfCheckedId)

  }

  //to check supplier item 
  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  //check all suppliers
  onAllChecked(value: boolean): void {
    this.listOfData.forEach((item: any) => this.updateCheckedSet(item.Id, value));
    this.refreshCheckedStatus();
  }
  onCurrentPageDataChange($event: any): void {
    //console.log($event)
    if (this.listOfData) {
      this.listOfData = $event;
      this.refreshCheckedStatus();
    }
  }

  //refresh status of checked suppliers
  refreshCheckedStatus(): void {
    if (this.listOfData) {
      this.checked = this.listOfData.every((item: any) => this.setOfCheckedId.has(item.Id));
      this.indeterminate = this.listOfData.some((item: any) => this.setOfCheckedId.has(item.Id)) && !this.checked;
    }
  }

  //to get all active suppliers
  getAllActiveSuppliers(month: any, year: any) {
    if (this.userSessionToken) {
      this.loading = true;
      this._gs.getActiveSuppliers(this.userSessionToken, month, year).subscribe(
        (response: any) => {
          this.listOfData = response.Result
          this.onCurrentPageDataChange(response.Result)
          this.loading = false;
          console.log(response)
          // this.listOfData.forEach((element:any) => {
          //   console.log(element)
          // });
        }
      );
    }
  }

  //to get custom header services names
  getCustomHeaders(month: any, year: any) {
    this._gs.getCustomHeaderNames(this.userSessionToken, month, year).subscribe(
      (response: any) => {
        debugger
        console.log(response)
        if (response.Result != null) {
          this.currentHeaderId = response.Result[0].Id;
          this.Custom1 = (response.Result[0].Custom1 != null) ? response.Result[0].Custom1 : "Custom 1";
          this.Custom2 = (response.Result[0].Custom2 != null) ? response.Result[0].Custom2 : "Custom 2";
          this.Custom3 = (response.Result[0].Custom3 != null) ? response.Result[0].Custom3 : "Custom 3";
          this.Custom4 = (response.Result[0].Custom4 != null) ? response.Result[0].Custom4 : "Custom 4";
          this.Custom5 = (response.Result[0].Custom5 != null) ? response.Result[0].Custom5 : "Custom 5";
        }
      }
    );
  }

  //add invoice data to database
  addInvoiceDataToDB() {
    this._gs.addInvoiceData(this.userSessionToken, this.listOfData).subscribe(
      (response: any) => console.log(response)
    );
    const updateHeader = {
      "Id": this.currentHeaderId,
      "InvoiceReferance": this.invoiceRef,
      "Custom1": this.Custom1,
      "Custom2": this.Custom2,
      "Custom3": this.Custom3,
      "Custom4": this.Custom4,
      "Custom5": this.Custom5,
      "InvoiceMonth": this.chosenMonthString,
      "InvoiceYear": this.chosenYear,
      "InvoiceDate": this.date
    }
    this._gs.updateCustomHeader(this.userSessionToken, updateHeader).subscribe(
      (response: any) => {
        console.log(response)
        Swal.fire('Invoices Saved!', '', 'success')
      }
    );
  }

  //approve selected invoices
  approveInvoices() {
    this._gs.approveInvoices(this.userSessionToken, this.arrayOfCheckedId).subscribe(
      (response: any) => console.log(response)
    );
    this.checked = true;
  }

  //t print report
  printPage() {
    window.print();
  }
}
