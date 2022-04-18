import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GGInvoiceService } from '../gginvoice.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {

  apiProductsData: GGInvoiceService["suppliers"][] = [];
  searchableData:any[]=[];
  validateForm: FormGroup;
  isVisibleTop: any;
  data: any;
  isEdited = false;
  searchText:any;
  sortNameFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.SupplierName.localeCompare(b.SupplierName);
  sortRefFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.ReferenceNumber.localeCompare(b.ReferenceNumber);
  userSessionToken = sessionStorage.getItem("User");  

  constructor(private fb: FormBuilder, private _gs: GGInvoiceService, private router:Router, private http: HttpClient) {
    this.validateForm = this.fb.group({
      supplierName: ['', [Validators.required, Validators.pattern("^[a-zA-Z]+[ ]?[a-zA-Z]+$")]],
      supplierReference: ['', [Validators.required, Validators.pattern("[A-Za-z0-9]{1,15}")]],
      businessAddress: ['', [Validators.required, Validators.pattern("")]],
      emailAddress: ['', [Validators.required]],
      phoneNumber: ['',Validators.pattern("^[0-9]{1,15}$")],
      companyRegisteredNumber:['', [Validators.pattern("^[0-9]{1,15}")]],
      VATNumber:['', [Validators.pattern("^GB[0-9]{9}")]],
      taxReference:['', [Validators.pattern("[A-Za-z0-9]{1,15}")]],
      companyRegisteredAddress:['',[Validators.pattern("")]],
      activeSupplier:[''],
    });
  }
  onGetSuppliers() {
    if (this.userSessionToken) {
      this._gs.getSuppliers(this.userSessionToken).subscribe(
        (response: any) => {
          this.apiProductsData = response.Result
          this.searchableData = response.Result
        }
      );
    }
  }
  onDeleteProduct(id: any) {
    console.log(id)
    Swal.fire({
      title: 'Do you want to delete this product?',
      showDenyButton: true,
      icon: 'question',
      confirmButtonText: 'Yes',
      denyButtonText: `Don't delete`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this._gs.deletetSupplier(id, this.userSessionToken).subscribe(
          //(response) => this.apiData = response
        );
        Swal.fire('Product Deleted!', '', 'success')

        this.apiProductsData = this.apiProductsData.filter((d: {SupplierId: any; }) => d.SupplierId !== id);
      } else if (result.isDenied) {
        Swal.fire('Product is safe!', '', 'info')
      }
    });
  }
  ngOnInit() {
    this.onGetSuppliers();
  }
  handleCancelTop(): void {
    this.isVisibleTop = false;
  }
  handleOkTop(): void {
    if (this.validateForm.valid) {
      if (this.data) {
        console.log(this.data)
        this.editSupplier(this.data.SupplierId);
      }
      else {
        this.addSupplierToDB();
      }
    }
    else {
      //to fire validations on invalid fields
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
    //this.displayEvent.emit(this.isVisibleTop);
  }
  addSupplierToDB() {
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = formValue.supplierName;
    this._gs.suppliers.ReferenceNumber = formValue.supplierReference;
    this._gs.suppliers.BusinessAddress = formValue.businessAddress;
    this._gs.suppliers.Email = formValue.emailAddress;
    this._gs.suppliers.Phone = formValue.phoneNumber;
    this._gs.suppliers.CompanyRegNumber = formValue.companyRegisteredNumber;
    this._gs.suppliers.VatNumber = formValue.VATNumber;
    this._gs.suppliers.TaxReference = formValue.taxReference;
    if (formValue.activeSupplier == null) {
      this._gs.suppliers.IsActive = false;
    } else {
      this._gs.suppliers.IsActive = formValue.activeSupplier;

    }
    console.log(this._gs.suppliers)
    this._gs.addSupplier(this.userSessionToken).subscribe(
      (response:any) => {
        console.log(this.apiProductsData)
        console.log(response.Result)
        this.apiProductsData.push(response.Result)
        console.log(this.apiProductsData)
      }
    );
    this.isVisibleTop = false;
  }
  showModalTop($event: any): void {
    this.isVisibleTop = true;
    if (!$event) {
      this.data = null
      this.validateForm.reset();
    }
    else {
      this.data = $event;
      this.validateForm.patchValue({
      supplierName: this.data.SupplierName,
      supplierReference: this.data.ReferenceNumber,
      businessAddress: this.data.BusinessAddress,
      emailAddress: this.data.Email,
      phoneNumber: this.data.Phone,
      companyRegisteredNumber:this.data.CompanyRegNumber,
      VATNumber: this.data.VatNumber,
      taxReference: this.data.taxReference,
      companyRegisteredAddress: this.data.CompanyRegAddress,
      activeSupplier:this.data.IsActive,
      });
    }
  }
  editSupplier(id: any) {
    console.log(id)
    debugger
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = this.data.SupplierName = formValue.supplierName;
    this._gs.suppliers.ReferenceNumber = this.data.ReferenceNumber = formValue.supplierReference;
    this._gs.suppliers.BusinessAddress = this.data.BusinessAddress = formValue.businessAddress;
    this._gs.suppliers.Email = this.data.Email = formValue.emailAddress;
    this._gs.suppliers.Phone = this.data.Phone = formValue.phoneNumber;
    this._gs.suppliers.CompanyRegNumber = this.data.CompanyRegNumber  = formValue.companyRegisteredNumber;
    this._gs.suppliers.VatNumber = this.data.VatNumber = formValue.VATNumber;
    this._gs.suppliers.TaxReference = this.data.taxReference = formValue.taxReference;
    
    this._gs.updateSupplier(id, this.userSessionToken).subscribe(
      //(response) => this.apiData.data.push(response)
    );
    this.isEdited = true;
    this.isVisibleTop = false;
  }
  closeAlert() {
    this.isEdited = false;
  }
  searchData(){
    this.apiProductsData = this.searchableData.filter((item: any) => item.name.indexOf(this.searchText) !== -1);
  }
  
}
