import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GGInvoiceService } from '../gginvoice.service';
import Swal from 'sweetalert2'
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  //constructor to inject services
  constructor(private message: NzMessageService,private fb: FormBuilder, private router: Router, private _gs: GGInvoiceService,) { }
  validateForm!: FormGroup;

  //submit button to login
  submitForm(): void {
    if (this.validateForm.valid) {
      this._gs.getLoginStatus(this.validateForm.value.userName, this.validateForm.value.password).subscribe(
        (response: any) => {
          console.log(response)
          if (response.Message == 'Success.') {
            this._gs.getToken(this.validateForm.value.userName, this.validateForm.value.password).subscribe(
              (response: any) => {
                console.log(response.access_token)
                if (response.access_token) {
                  this.router.navigate(['/monthly-invoice']);
                  sessionStorage.setItem("User", response.access_token);
                }
              },
              (error) => {
                Swal.fire({
                  title: error.error.error_description,
                  icon: 'error',
                  confirmButtonText: 'Okay',
                  confirmButtonColor: '#DC3741',
                })
              }
            );
          } else {
            Swal.fire({
              title: "Username or Password is Incorrect!",
              icon: 'error',
              confirmButtonText: 'Okay',
              confirmButtonColor: '#DC3741',
            })
          }
        },
      );
    } else {
      //if values in form are invalid then fire validations
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  ngOnInit(): void {
    //validating form
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });

    //check if use already logged in
    if(sessionStorage.getItem("User") != null){
      this.message.create('error', 'You\'re already logged in.')
      this.router.navigate(['/monthly-invoice']);
    }
  }

}
