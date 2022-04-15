import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GGInvoiceService } from '../gginvoice.service';

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
  //sortPriceFn = (a: GGInvoiceService["suppliers"], b: GGInvoiceService["suppliers"]): number => a.Price - b.Price;
  userSessionToken = sessionStorage.getItem("User");

  

  constructor(private fb: FormBuilder, private _gs: GGInvoiceService, private router:Router) {
    this.validateForm = this.fb.group({
      productName: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9 ]{1,20}$")]],
      productPrice: ['', [Validators.required, Validators.pattern("^[0-9]{1,10}.?[0-9]{1,10}$")]],
      productQuantity: ['', [Validators.required, Validators.pattern("^[0-9]{1,5}$")]],
      productDescription: ['', [Validators.required, Validators.pattern("^^[A-Za-z0-9]{1,50}[A-Za-z0-9 ]{1,10}[A-Za-z0-9]{1,50}$")]],
      productStatus: [''],
      userName:[''],
    });
  }
  onGetProducts() {
    if (this.userSessionToken) {
      this._gs.getProducts(this.userSessionToken).subscribe(
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
        this._gs.deletetProduct(id, this.userSessionToken).subscribe(
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
    if(!this.userSessionToken){
      this.router.navigate(['/login'])
    }
    this.onGetProducts();
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
    // debugger
    // const formValue = this.validateForm.value;
    // this._gs.suppliers.Name = formValue.productName;
    // this._gs.suppliers.Description = formValue.productDescription;
    // this._gs.suppliers.Price = formValue.productPrice;
    // this._gs.suppliers.OnHandQuantity = formValue.productQuantity;
    // if (formValue.productStatus == null) {
    //   this._gs.suppliers.Status = false;
    // } else {
    //   this._gs.suppliers.Status = formValue.productStatus;
    // }
    // console.log(this._gs.suppliers)
    // this._gs.addProduct(this.userSessionToken).subscribe(
    //   (response:any) => this.apiProductsData.push(response.Result)
    // );
    // this.isVisibleTop = false;
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
    // debugger
    // const formValue = this.validateForm.value;
    // this._gs.suppliers.Name = this.data.Name = formValue.productName;
    // this._gs.suppliers.Description = this.data.Description = formValue.productDescription;
    // this._gs.suppliers.Price = this.data.Price = formValue.productPrice;
    // this._gs.suppliers.OnHandQuantity = this.data.OnHandQuantity = formValue.productQuantity;
    // this._gs.suppliers.Status = this.data.Status = formValue.productStatus;
    // this._gs.updateProduct(id, this.userSessionToken).subscribe(
    //   //(response) => this.apiData.data.push(response)
    // );
    // this.isEdited = true;
    // this.isVisibleTop = false;
  }
  closeAlert() {
    this.isEdited = false;
  }
  searchData(){}
}
