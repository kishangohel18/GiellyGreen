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
    "SupplierName": "sample string 2",
    "ReferenceNumber": "sample string 3",
    "BusinessAddress": "sample string 4",
    "Email": "sample string 5",
    "Phone": "sample string 6",
    "TaxReference": "sample string 7",
    "CompanyRegNumber": "sample string 8",
    "CompanyRegAddress": "sample string 9",
    "VatNumber": "sample string 10",
    "CreatedDate": "2022-04-14T19:08:10.78912+05:30",
    "ModifiedDate": "2022-04-14T19:08:10.78912+05:30",
    "Logo": "QEA=",
    "IsActive": true
  }
  constructor(private http: HttpClient) { }

  getLoginStatus(username: any, password: any): Observable<unknown>{
    let user = {"Email" : username, "password":password}
    return this.http.post<unknown>(`http://7cd1-106-201-236-89.ngrok.io/api/Login`, user);
  }

  getToken(username: any, password: any): Observable<unknown> {
    let body = new URLSearchParams();
    body.set('grant_type', 'password')
    body.set('username', username);
    body.set('password', password);

    let RequestOptions = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.http.post<unknown>(`http://7cd1-106-201-236-89.ngrok.io/token`, body, RequestOptions);
  }
  getProducts(userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.get<unknown>(`http://7cd1-106-201-236-89.ngrok.io/api/Suppliers`,{headers:header});
  }
  addProduct(userSessionToken:any): Observable<unknown> {
    debugger
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.post<unknown>(`http://2eb2-106-201-236-89.ngrok.io/api/Product`, this.suppliers, {headers:header});
  }
  updateProduct(id:any, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.put<unknown>(`http://2eb2-106-201-236-89.ngrok.io/api/Product/${id}`, this.suppliers, {headers:header});
  }
  deletetProduct(id: number, userSessionToken:any): Observable<unknown> {
    const header = {"Authorization":"bearer "+userSessionToken}
    return this.http.delete<unknown>(`http://7cd1-106-201-236-89.ngrok.io/api/Product/${id}`, {headers:header});
  }
}
