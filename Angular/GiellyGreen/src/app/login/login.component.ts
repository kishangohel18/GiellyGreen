import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GGInvoiceService } from '../gginvoice.service';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginStatus: any;

  constructor(private fb: FormBuilder, private router: Router, private _gs: GGInvoiceService) { }
  validateForm!: FormGroup;

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
                  confirmButtonText: 'Okay'
                })
              }
            );
          } else {
            Swal.fire({
              title: "Username or Password is Incorrect!",
              icon: 'error',
              confirmButtonText: 'Okay'
            })
          }
        },
      );
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
    sessionStorage.removeItem("User");
  }

}
