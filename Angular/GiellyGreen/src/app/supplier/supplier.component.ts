import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GGInvoiceService } from '../gginvoice.service';
import { HttpClient } from '@angular/common/http';
import { faPen, faTrash, faPlus } from '@fortawesome/free-solid-svg-icons';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {

  apiSuppliersData: GGInvoiceService["suppliers"][] = [];
  faPen = faPen;
  faTrash = faTrash;
  faPlus = faPlus;
  searchedData: any;
  validateForm: FormGroup;
  supplierLogo: any;
  isVisibleTop: any;
  baseURL: any;
  data: any;
  uploadedSupplierLogo:any
  file: File;
  // isEdited = false;
  searchText: any;
  sortNameFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.SupplierName.localeCompare(b.SupplierName);
  sortRefFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.ReferenceNumber.localeCompare(b.ReferenceNumber);
  userSessionToken = sessionStorage.getItem("User");

  constructor(private message: NzMessageService, private fb: FormBuilder, private _gs: GGInvoiceService, private router: Router, private http: HttpClient) {
    this.validateForm = this.fb.group({
      supplierName: ['', [Validators.required, Validators.pattern("^[a-zA-Z]+[ ]?[a-zA-Z]+$")]],
      supplierReference: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9]{1,15}$")]],
      businessAddress: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9 .-]{3,150}$")]],
      emailAddress: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern("^[0-9]{1,15}$")],
      companyRegisteredNumber: ['', [Validators.pattern("^[0-9a-zA-Z]{1,15}$")]],
      VATNumber: ['', [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      taxReference: ['', [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      companyRegisteredAddress: ['', [Validators.pattern("[A-Za-z0-9 ]{3,150}$")]],
      activeSupplier: [''],
      supplierLogo: [''],
    });
  }
  onGetSuppliers() {
    if (this.userSessionToken) {
      this._gs.getSuppliers(this.userSessionToken).subscribe(
        (response: any) => {
          this.apiSuppliersData = response.Result
        }
      );
    }
  }
  onDeleteProduct(data: any) {
    Swal.fire({
      title: 'Are You Sure?',
      showDenyButton: true,
      icon: 'error',
      text: 'Once the record is deleted, this process cannot be undone!',
      denyButtonText: `Cancel`,
      denyButtonColor: '#CFD3D8',
      confirmButtonText: 'Delete',
      confirmButtonColor: '#FF8080',
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        // if(data.MonthlyInvoice.length == 0){
        this._gs.deletetSupplier(data.SupplierId, this.userSessionToken).subscribe(
          (response: any) => {
            if (response.ResponseStatus == 2) {
              data.IsActive = false;
              this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, data.IsActive).subscribe();
              Swal.fire('Supplier Inactivated!', '', 'success')
            }
            else {
              Swal.fire('Supplier Deleted!', '', 'success')
              this.apiSuppliersData = this.apiSuppliersData.filter((d: { SupplierId: any; }) => d.SupplierId !== data.SupplierId);
            }
          }
        );
      } else if (result.isDenied) {
        Swal.fire('Supplier is safe!', '', 'info')
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
        this.editSupplier(this.data);
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
    this._gs.suppliers.CompanyRegAddress = formValue.companyRegisteredAddress;
    if (formValue.activeSupplier == null) {
      this._gs.suppliers.IsActive = false;
    } else {
      this._gs.suppliers.IsActive = formValue.activeSupplier;
    }
    this._gs.suppliers.LogoUrl = this.supplierLogo
    console.log(this._gs.suppliers)
    this._gs.uniqueMail(0, this._gs.suppliers.Email).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 0) {
          this.message.create('error', 'This Email already taken, please enter another one.')
        } else {
          this._gs.uniqueTaxRef(0, this._gs.suppliers.TaxReference).subscribe(
            (response: any) => {
              if (response.ResponseStatus == 0) {
                this.message.create('error', 'Tax reference number already taken, please enter another one.')
              } else {
                this._gs.uniqueVAT(0, this._gs.suppliers.VatNumber).subscribe(
                  (response: any) => {
                    if (response.ResponseStatus == 0) {
                      this.message.create('error', 'This VAT number is used, Please enter unique VAT Number.')
                    } else {
                      this._gs.uniqueSupplierRef(0, this._gs.suppliers.ReferenceNumber).subscribe(
                        (response: any) => {
                          if (response.ResponseStatus == 0) {
                            this.message.create('error', 'Supplier reference number already taken, please enter another one.')
                          } else {
                            this._gs.addSupplier(this.userSessionToken).subscribe(
                              (response: any) => {
                                console.log(this.apiSuppliersData)
                                this.apiSuppliersData.push(response.Result)
                              }
                            );
                            this.isVisibleTop = false;
                            this.onGetSuppliers();
                          }
                        }
                      );
                    }
                  }
                );
              }
            }
          );
        }
      }
    );
  }
  showModalTop($event: any): void {
    this.isVisibleTop = true;
    if (!$event) {
      this.data = null
      this.validateForm.reset();
    }
    else {

      this.data = $event;

      console.log(this.data)
      this.validateForm.patchValue({
        supplierName: this.data.SupplierName,
        supplierReference: this.data.ReferenceNumber,
        businessAddress: this.data.BusinessAddress,
        emailAddress: this.data.Email,
        phoneNumber: this.data.Phone,
        companyRegisteredNumber: this.data.CompanyRegNumber,
        VATNumber: this.data.VatNumber,
        taxReference: this.data.TaxReference,
        companyRegisteredAddress: this.data.CompanyRegAddress,
        activeSupplier: this.data.IsActive,
        supplierLogo: this.uploadedSupplierLogo,
      });
    }
  }
  editSupplier(data: any) {
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = this.data.SupplierName = formValue.supplierName;
    this._gs.suppliers.ReferenceNumber = this.data.ReferenceNumber = formValue.supplierReference;
    this._gs.suppliers.BusinessAddress = this.data.BusinessAddress = formValue.businessAddress;
    this._gs.suppliers.Email = this.data.Email = formValue.emailAddress;
    this._gs.suppliers.Phone = this.data.Phone = formValue.phoneNumber;
    this._gs.suppliers.CompanyRegNumber = this.data.CompanyRegNumber = formValue.companyRegisteredNumber;
    this._gs.suppliers.VatNumber = this.data.VatNumber = formValue.VATNumber;
    this._gs.suppliers.TaxReference = this.data.taxReference = formValue.taxReference;
    if (formValue.activeSupplier == null) {
      this._gs.suppliers.IsActive = false;
    } else {
      this._gs.suppliers.IsActive = this.data.IsActive = formValue.activeSupplier;
    }
    this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, formValue.activeSupplier).subscribe();
    this._gs.updateSupplier(data.SupplierId, this.userSessionToken).subscribe();
    // this.isEdited = true;
    this.isVisibleTop = false;
  }
  closeAlert() {
    // this.isEdited = false;
  }
  searchSupplier() {
    this.searchedData = this.apiSuppliersData;
    this.apiSuppliersData = this.searchedData.filter((item: any) => item.SupplierName.indexOf(this.searchText) !== -1);
    if (this.searchText.length == 0) {
      this.onGetSuppliers();
    }
  }
  changeStatus(data: any, supplierStatus: any) {
    console.log(supplierStatus)
    this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, supplierStatus).subscribe();
  }

  UploadLogo(event: any) {
    this.file = event.target.files[0];
    const reader: any = new FileReader();
    reader.readAsDataURL(this.file);
    reader.onload = () => {
      this.baseURL = reader.result.split(",")
      this.supplierLogo = this.baseURL[1];
      this.uploadedSupplierLogo = "data:image/png;base64,"+this.supplierLogo;
    };
  }
}
