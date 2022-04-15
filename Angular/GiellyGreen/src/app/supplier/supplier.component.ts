import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GGInvoiceService } from '../gginvoice.service';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {

  apiProductsData: GGInvoiceService["suppliers"][] = [];
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
      supplierName: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9 ]{1,20}$")]],
      supplierReference: ['', [Validators.required]],
      businessAddress: ['', [Validators.required]],
      emailAddress: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      companyRegisteredNumber:[''],
      VATNumber:[''],
      taxReference:[''],
      companyRegisteredAddress:[''],
      activeSupplier:[''],
    });
  }
  onGetSuppliers() {
    if (this.userSessionToken) {
      this._gs.getSuppliers(this.userSessionToken).subscribe(
        (response: any) => this.apiProductsData = response.Result
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
        this.editProduct(this.data.Id);
      }
      else {
        this.addProduct();
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
  addProduct() {
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
      (response:any) => this.apiProductsData.push(response.Result)
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
        productName: this.data.Name,
        productDescription: this.data.Description,
        productPrice: this.data.Price,
        productQuantity: this.data.OnHandQuantity,
        productStatus: this.data.Status,
      });
    }
  }
  editProduct(id: any) {
    debugger
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = this.data.SupplierName = formValue.supplierName;
    this._gs.suppliers.ReferenceNumber = this.data.ReferenceNumber = formValue.supplierReference;
    this._gs.suppliers.BusinessAddress = this.data.BusinessAddress = formValue.businessAddress;
    this._gs.suppliers.Email =this.data.Email = formValue.emailAddress;
    this._gs.suppliers.Phone = formValue.phoneNumber;
    this._gs.suppliers.CompanyRegNumber = formValue.companyRegisteredNumber;
    this._gs.suppliers.VatNumber = formValue.VATNumber;
    this._gs.suppliers.TaxReference = formValue.taxReference;
    
    this._gs.updateProduct(id, this.userSessionToken).subscribe(
      //(response) => this.apiData.data.push(response)
    );
    this.isEdited = true;
    this.isVisibleTop = false;
  }
  closeAlert() {
    this.isEdited = false;
  }
  searchData(){}
  previewFile = (file: NzUploadFile): Observable<string> => {
    console.log('Your upload file:', file);
    return this.http
      .post<{ thumbnail: string }>(`https://next.json-generator.com/api/json/get/4ytyBoLK8`, {
        method: 'POST',
        body: file
      })
      .pipe(map(res => res.thumbnail));
  };
}
