import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GGInvoiceService {

  suppliers={
    "SupplierId": 1,
    "SupplierName": "",
    "ReferenceNumber": "",
    "BusinessAddress": "",
    "Email": "",
    "Phone": "",
    "TaxReference": "",
    "CompanyRegNumber": "",
    "CompanyRegAddress": "",
    "VatNumber": "",
    "CreatedDate": "",
    "ModifiedDate": "",
    "LogoUrl": "",
    "IsActive": false
  }

  constructor(private http: HttpClient) { }

  getLoginStatus(username: any, password: any): Observable<unknown>{
    let user = {"Email" : username, "password":password}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Login`, user);
  }

  getToken(username: any, password: any): Observable<unknown> {
    let body = new URLSearchParams();
    body.set('grant_type', 'password')
    body.set('username', username);
    body.set('password', password);

    let RequestOptions = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/token`, body, RequestOptions);
  }
  getSuppliers(userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.get<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Suppliers`,{headers:header});
  }
  addSupplier(userSessionToken:any): Observable<unknown> {
    debugger
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Suppliers`, this.suppliers, {headers:header});
  }
  updateSupplier(id:any, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.put<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Suppliers/${id}`, this.suppliers, {headers:header});
  }
  deletetSupplier(id: number, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.delete<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Suppliers/${id}`, {headers:header});
  }
  updateSupplierStatus(id:number, userSessionToken:any, supplierStatus:any){
    console.log(supplierStatus)
    const header = {"Authorization":"bearer "+userSessionToken}
    let newSupplierStatus = {"SupplierId" : id, "IsActive":supplierStatus}
    return this.http.patch<unknown>(`http://c4f3-106-201-236-89.ngrok.io/api/Suppliers/${id}`, newSupplierStatus, {headers:header});
  }
  uniqueMail(id:number, userSessionToken:any, email:any){
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/VarifyEmail?id=${id}&email=${email}`, {headers:header});
  }
  uniqueTaxRef(id:number, userSessionToken:any, taxRef:any){
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/VarifyEmail?id=${id}&email=${taxRef}`, {headers:header});
  }
  uniqueVAT(id:number, userSessionToken:any, VAT:any){
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/VarifyEmail?id=${id}&email=${VAT}`, {headers:header});
  }
  uniqueSupplierRef(id:number, userSessionToken:any, supRef:any){
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://c4f3-106-201-236-89.ngrok.io/VarifyEmail?id=${id}&email=${supRef}`, {headers:header});
  }
}
