import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GGInvoiceService {

  constructor(private http: HttpClient) { }

  getLoginStatus(username: any, password: any): Observable<unknown>{
    let user = {username : username, password:password}
    return this.http.post<unknown>(`http://3fb2-106-201-236-89.ngrok.io/api/Login`, user);
  }

  getToken(username: any, password: any): Observable<unknown> {
    let body = new URLSearchParams();
    body.set('grant_type', 'password')
    body.set('username', username);
    body.set('password', password);

    let RequestOptions = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.http.post<unknown>(`http://3fb2-106-201-236-89.ngrok.io/token`, body, RequestOptions);
  }
}
