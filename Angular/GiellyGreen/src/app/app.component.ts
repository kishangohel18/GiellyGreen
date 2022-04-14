import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'GiellyGreen';
  validateForm!: FormGroup;

  submitForm(): void{
    if (this.validateForm.valid) {
      // this.eventService.getToken(this.validateForm.value.userName,this.validateForm.value.password).subscribe(
      //   (response: any)=>{
      //     console.log(response.access_token)
      //     if(response.access_token){
      //       this.router.navigate(['/products']);
      //       sessionStorage.setItem("User", response.access_token);
      //     }
        // },
        // (error)=>{
        //   Swal.fire({
        //     title: error.error.error_description,
        //     icon: 'error',
        //     confirmButtonText: 'Okay'
        //   })
        // }
        // );
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
  constructor(private fb: FormBuilder, private router: Router) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
    sessionStorage.removeItem("User");
  }
}
