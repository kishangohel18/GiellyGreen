import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MonthlyInvoiceComponent } from './monthly-invoice/monthly-invoice.component';
import { SupplierComponent } from './supplier/supplier.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  {path : 'supplier', component: SupplierComponent},
  {path : 'monthly-invoice', component: MonthlyInvoiceComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
