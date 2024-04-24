import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const url = 'your_auth_endpoint';
    const body = { username, password };
    return this.http.post(url, body);
  }
}
