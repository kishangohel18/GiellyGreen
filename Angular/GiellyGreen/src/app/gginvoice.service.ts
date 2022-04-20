import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GGInvoiceService {

  suppliers={
    "SupplierId": 0,
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

  apiURL = "http://5d35-106-201-236-89.ngrok.io"
  constructor(private http: HttpClient) { }

  //to check whether username and password are correct
  getLoginStatus(username: any, password: any): Observable<unknown>{
    let user = {"Email" : username, "password":password}
    return this.http.post<unknown>(`${this.apiURL}/api/Login`, user);
  }

  //to get access token
  getToken(username: any, password: any): Observable<unknown> {
    let body = new URLSearchParams();
    body.set('grant_type', 'password')
    body.set('username', username);
    body.set('password', password);

    let RequestOptions = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.http.post<unknown>(`${this.apiURL}/token`, body, RequestOptions);
  }

  //to get all suppliers from DB
  getSuppliers(userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.get<unknown>(`${this.apiURL}/api/Suppliers`,{headers:header});
  }

  //add supplier to DB
  addSupplier(userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/api/Suppliers`, this.suppliers, {headers:header});
  }

  //update supplier to DB
  updateSupplier(id:any, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.put<unknown>(`${this.apiURL}/api/Suppliers/${id}`, this.suppliers, {headers:header});
  }

  //delete supplier from DB
  deletetSupplier(id: number, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.delete<unknown>(`${this.apiURL}/api/Suppliers/${id}`, {headers:header});
  }

  //change active/inactive status of supplier
  updateSupplierStatus(id:number, userSessionToken:any, supplierStatus:any){
    console.log(supplierStatus)
    const header = {"Authorization":"bearer "+userSessionToken}
    let newSupplierStatus = {"SupplierId" : id, "IsActive":supplierStatus}
    return this.http.patch<unknown>(`${this.apiURL}/api/Suppliers/${id}`, newSupplierStatus, {headers:header});
  }

  //check whether mail is unique
  uniqueMail(id:number,email:any){
    //const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/VarifyEmail?id=${id}&email=${email}`,null);
  }

  //check whether Tax reference is unique
  uniqueTaxRef(id:number, taxRef:any){
    //const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/VarifyTaxReference?id=${id}&TaxReference=${taxRef}`, null);
  }

  //check whether VAT is unique
  uniqueVAT(id:number, VAT:any){
    //const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/VarifyVatNumber?id=${id}&VatNumber=${VAT}`, null);
  }

  //check whether supplier reference is unique
  uniqueSupplierRef(id:number,supRef:any){
    //const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/VarifyRefenceNumber?id=${id}&ReferenceNumber=${supRef}`, null);
  }

  //get all active suppliers
  getActiveSuppliers(userSessionToken:any,month:any,year:any){
    console.log(month+year)
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.get<unknown>(`${this.apiURL}/GetAllSupplierByIsActive?month=${month}&year=${year}`,{headers:header});
  }

  //get header custom service names
  getCustomHeaderNames(userSessionToken:any,month:any,year:any){
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.get<unknown>(`${this.apiURL}/GetCustomHeaderByDate?month=${month}&year=${year}`,{headers:header});
  }

  //add updated invoice data to DB
  addInvoiceData(userSessionToken:any, listOfData:any): Observable<unknown>{
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/InsetUpdateMonthly_Invoice`, listOfData, {headers:header});
  }

  //approve selected invoices
  approveInvoices(userSessionToken:any,checkedIds:any): Observable<unknown>{
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/ApproveSelectedInvoice`, checkedIds, {headers:header});
  }

  //update header service names to DB
  updateCustomHeader(userSessionToken:any, headerBody:any): Observable<unknown>{
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`${this.apiURL}/InsertUpdateCustomHeader`, headerBody , {headers:header});
  }
}
