import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

const httpOptions = {
  Headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMemebers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions)
  }

  getMemeber(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username, httpOptions)
  }
}
