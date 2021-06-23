import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMemebers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', {
      headers: {
        Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
      }
    })
  }

  getMemeber(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username, {
      headers: {
        Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
      }
    })
  }
}
