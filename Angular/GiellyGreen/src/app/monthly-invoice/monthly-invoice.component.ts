import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { GGInvoiceService } from '../gginvoice.service';
import Swal from 'sweetalert2';
import { DecimalPipe } from '@angular/common';

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
  listOfData: any[] = [];
  currentHeaderId: any;
  chosenMonthString: any;
  chosenYear: any
  editId: any;
  arrayOfCheckedId: any;
  loading = true;
  curMonth: any;
  individualNet: any;
  individualVat: any;


  userSessionToken = sessionStorage.getItem("User");
  constructor(public router: Router, private fb: FormBuilder, private _gs: GGInvoiceService, public decimal: DecimalPipe) {

  }
  ngOnInit(): void {
  }
  onChange(result: Date): void {
    this.curMonth = result.getMonth() + 1
    this.chosenYear = result.getFullYear();
    const chosenMonth = String(result.getMonth() + 1);
    if (chosenMonth.length == 1) {
      this.chosenMonthString = "0" + chosenMonth;
    }
    else {
      this.chosenMonthString = chosenMonth + "";
    }
    this.date = new Date(this.chosenYear,result.getMonth() + 1,0);

    this.invoiceRef = result.toLocaleString('en-us', { month: 'long' }) + "" + this.chosenYear;

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
    this.listOfData.forEach((item: any) => this.updateCheckedSet(item.SupplierId, value));
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
      this.checked = this.listOfData.every((item: any) => this.setOfCheckedId.has(item.SupplierId));
      this.indeterminate = this.listOfData.some((item: any) => this.setOfCheckedId.has(item.SupplierId)) && !this.checked;
    }
  }

  //to get all active suppliers
  getAllActiveSuppliers(month: any, year: any) {
    if (this.userSessionToken) {
      this.loading = true;
      this._gs.getActiveSuppliers(this.userSessionToken, month, year).subscribe(
        (response: any) => {
          console.log(response)
          this.listOfData = response.Result
          this.onCurrentPageDataChange(response.Result)
          this.loading = false;
        }
      );
    }
  }

  //to get custom header services names
  getCustomHeaders(month: any, year: any) {
    this._gs.getCustomHeaderNames(this.userSessionToken, month, year).subscribe(
      (response: any) => {
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
      //(response: any) => console.log(response)
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
        Swal.fire('Invoices Saved!', '', 'success')
      }
    );
  }

  //approve selected invoices
  approveInvoices() {
    this._gs.approveInvoices(this.userSessionToken, this.arrayOfCheckedId).subscribe(
      //(response: any) => console.log(response)
    );
    //this.checked = true;
  }
  emailInvoices(){
    debugger
    console.log(this.arrayOfCheckedId)
    this._gs.sendEmails(this.userSessionToken, this.arrayOfCheckedId).subscribe(
      (response)=>{

        console.log(response)
      }
    )
  }

  //t print report
  printPage() {
    window.print();
  }
  getNet(data: any) { 
    return data.Net=data.HairService + data.BeautyService + data.Custom1 +
      data.Custom2
      + data.Custom3 + data.Custom4 + data.Custom5;
  }
  getVat(data: any) {
    return data.Vat || (data.HairService +
      data.BeautyService + data.Custom1 + data.Custom2
      + data.Custom3 + data.Custom4 + data.Custom5) * (0.2)
  }
  getGross(data: any) {
    return data.Gross || (data.HairService + data.BeautyService + data.Custom1 +
      data.Custom2
      + data.Custom3 + data.Custom4 + data.Custom5) + (data.HairService +
        data.BeautyService + data.Custom1 + data.Custom2
        + data.Custom3 + data.Custom4 + data.Custom5) * (0.2)
  }
  countTotalNet() {
    let totalNet=0;
    this.listOfData.forEach((data: any) => {
      if (data.Net != null) {
        totalNet += data.Net
      }
    });
    return totalNet
  }
  countTotalVat() {
    let totalVat=0;
    this.listOfData.forEach((data: any) => {
      if (data.Vat != null) {
        totalVat += data.Vat
      }
    });
    return totalVat
  }
  countTotalGross(){
    let totalGross=0;
    this.listOfData.forEach((data: any) => {
      if (data.Vat != null) {
        totalGross += data.Gross
      }
    });
    return totalGross
  }
  countTotalAdvancedPaid(){
    let totalAdvanced=0;
    this.listOfData.forEach((data: any) => {
      if (data.Vat != null) {
        totalAdvanced += data.Gross
      }
    });
    return totalAdvanced
  }
  countTotalBalanceDue(){
    let totalBalance=0;
    this.listOfData.forEach((data: any) => {
      if (data.Vat != null) {
        totalBalance += data.Gross
      }
    });
    return totalBalance
  }
  downloadCombinedPDF(){
    this._gs.combineAndDownloadPDF(this.userSessionToken,this.arrayOfCheckedId).subscribe(
      (response:any)=>{
        console.log(response)
        let base64String = response.Result;
        this.downloadPdf(base64String,"Combined");
      }
    );
  }
  downloadPdf(base64String:any, fileName:any) {
    const source = `data:application/pdf;base64,${base64String}`;
    const link = document.createElement("a");
    link.href = source;
    link.download = `${fileName}.pdf`
    link.click();
  }
}
