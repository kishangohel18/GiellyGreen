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

  //#region properties
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
  loading = true;
  uploadedSupplierLogo: any
  file: File;;
  searchText: any;

  //to sort column data of table
  sortNameFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.SupplierName.localeCompare(b.SupplierName);
  sortRefFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]) => a.SupplierReference.localeCompare(b.SupplierReference);

  //get user from session
  userSessionToken = sessionStorage.getItem("User");

  //constructor to inject services and validate modal form
  constructor(private message: NzMessageService, private fb: FormBuilder, private _gs: GGInvoiceService, private router: Router, private http: HttpClient) {
    this.validateForm = this.fb.group({
      supplierName: ['', [Validators.required, Validators.pattern("^[a-zA-Z]+[ ]?[a-zA-Z]+$")]],
      supplierReference: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9]{1,15}$")]],
      businessAddress: ['', [Validators.required, Validators.pattern("^[A-Za-z,0-9 .-]{3,150}$")]],
      emailAddress: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern("^[0-9]{1,15}$")],
      companyRegisteredNumber: ['', [Validators.pattern("^[0-9a-zA-Z]{1,15}$")]],
      VATNumber: ['', [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      taxReference: ['', [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      companyRegisteredAddress: ['', [Validators.pattern("[A-Za-z,0-9 .-]{3,150}$")]],
      activeSupplier: [''],
      fileUpload: [''],
    });
  }

  //get all suppliers from DB
  onGetSuppliers() {
    if (this.userSessionToken) {
      this.loading = true;
      this._gs.getSuppliers(this.userSessionToken).subscribe(
        (response: any) => {
          this.apiSuppliersData = response.Result
          console.log(this.apiSuppliersData)
          this.loading = false;
        }
      );
    }
  }

  //delete supplier or inactivate supplier from DB
  onDeleteSupplier(data: any) {
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
      if (result.isConfirmed) {
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

  //get suppliers on initialization
  ngOnInit() {
    this.onGetSuppliers();
  }

  //modal form cancel button to close modal
  handleCancelTop(): void {
    this.isVisibleTop = false;
    this.validateForm.reset();
  }

  //modal ok button to save data to DB
  handleOkTop(): void {
    if (this.validateForm.valid) {
      //if modal has data already edit current data
      if (this.data) {
        this.editSupplier(this.data);
        this.validateForm.reset();
      }
      else {
        //function to add new data to DB
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

  //add supplier data to database
  addSupplierToDB() {
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = formValue.supplierName;
    this._gs.suppliers.SupplierReference = formValue.supplierReference;
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
    if (this.supplierLogo) {
      this._gs.suppliers.LogoUrl = this.supplierLogo
    }

    //check validations
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
                      this._gs.uniqueSupplierRef(0, this._gs.suppliers.SupplierReference).subscribe(
                        (response: any) => {
                          if (response.ResponseStatus == 0) {
                            this.message.create('error', 'Supplier reference number already taken, please enter another one.')
                          } else {
                            this._gs.addSupplier(this.userSessionToken).subscribe(
                              (response: any) => {
                                console.log(this.apiSuppliersData)
                                this.apiSuppliersData.push(response.Result)
                                console.log(response)
                                this.onGetSuppliers();
                              },
                              (error:any)=>{
                                this.message.create("error","Data cannot be saved, check your fields again!");
                              }
                            );
                            this.isVisibleTop = false;
                            this.validateForm.reset();
                            this.uploadedSupplierLogo = null
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
    console.log(this._gs.suppliers)
  }

  //to show modal
  showModalTop($event: any): void {
    this.isVisibleTop = true;
    if (!$event) {
      this.data = null
      //this.validateForm.reset();
    }
    else {
      this.data = $event;
      console.log(this.data)

      //patch value to modal while editing
      this.uploadedSupplierLogo = "data:image/png;base64," + this.data.LogoUrl;
      this.validateForm.patchValue({
        supplierName: this.data.SupplierName,
        supplierReference: this.data.SupplierReference,
        businessAddress: this.data.BusinessAddress,
        emailAddress: this.data.Email,
        phoneNumber: this.data.Phone,
        companyRegisteredNumber: this.data.CompanyRegNumber,
        VATNumber: this.data.VatNumber,
        taxReference: this.data.TaxReference,
        companyRegisteredAddress: this.data.CompanyRegAddress,
        activeSupplier: this.data.IsActive,
        fileUpload: "data:image/png;base64," + this.data.LogoUrl,
      });
    }
  }

  //to edit supplier
  editSupplier(data: any) {
    console.log(data)
    this.uploadedSupplierLogo = "data:image/png;base64," + this.data.LogoUrl;
    const formValue = this.validateForm.value;
    this._gs.suppliers.SupplierName = this.data.SupplierName = formValue.supplierName;
    this._gs.suppliers.SupplierReference = this.data.supplierReference = formValue.supplierReference;
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
    //this._gs.suppliers.LogoUrl = this.data.LogoUrl = formValue.LogoUrl;
      // this._gs.uniqueMail(0, this._gs.suppliers.Email).subscribe(
      //   (response: any) => {
      //     if (response.ResponseStatus == 0) {
      //       this.message.create('error', 'This Email already taken, please enter another one.')
      //     } else {
      //       this._gs.uniqueTaxRef(0, this._gs.suppliers.TaxReference).subscribe(
      //         (response: any) => {
      //           if (response.ResponseStatus == 0) {
      //             this.message.create('error', 'Tax reference number already taken, please enter another one.')
      //           } else {
      //             this._gs.uniqueVAT(0, this._gs.suppliers.VatNumber).subscribe(
      //               (response: any) => {
      //                 if (response.ResponseStatus == 0) {
      //                   this.message.create('error', 'This VAT number is used, Please enter unique VAT Number.')
      //                 } else {
      //                   this._gs.uniqueSupplierRef(0, this._gs.suppliers.SupplierReference).subscribe(
      //                     (response: any) => {
      //                       if (response.ResponseStatus == 0) {
      //                         this.message.create('error', 'Supplier reference number already taken, please enter another one.')
      //                       } else {
      //                         this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, formValue.activeSupplier).subscribe();
      // this._gs.updateSupplier(data.SupplierId, this.userSessionToken).subscribe();
      // // this.isEdited = true;
      // this.isVisibleTop = false;
      //                       }
      //                     }
      //                   );
      //                 }
      //               }
      //             );
      //           }
      //         }
      //       );
      //     }
      //   }
      // );
    this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, formValue.activeSupplier).subscribe();
    this._gs.updateSupplier(data.SupplierId, this.userSessionToken).subscribe();
    // this.isEdited = true;
    this.isVisibleTop = false;
    
  }

  //search supplier
  searchSupplier() {
    this.searchedData = this.apiSuppliersData;
    this.apiSuppliersData = this.searchedData.filter((item: any) => item.SupplierName.indexOf(this.searchText) !== -1);
    if(this.searchText.length == 0) {
      this.onGetSuppliers();
    }
  }

  //change status of supplier from mail screen table
  changeStatus(data: any) {
    console.log(data.IsActive)
    this._gs.updateSupplierStatus(data.SupplierId, this.userSessionToken, data.IsActive).subscribe();
  }

  //upload supplier logo
  UploadLogo(event: any) {
    this.file = event.target.files[0];
    const reader: any = new FileReader();
    reader.readAsDataURL(this.file);
    reader.onload = () => {
      this.baseURL = reader.result.split(",")
      this.supplierLogo = this.baseURL[1];
      this.uploadedSupplierLogo = "data:image/png;base64," + this.supplierLogo;
    };
  }
  removeLogo(){
    this.uploadedSupplierLogo = null;
  }
}
