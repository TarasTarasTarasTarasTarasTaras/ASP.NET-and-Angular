import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + 'accounts/login/'
  private registerPath = environment.apiUrl + 'accounts/register/'
  private currentUserPath = environment.apiUrl + 'accounts/currentUser/'
  private getUserByIdPath = environment.apiUrl + 'accounts/user/'

  constructor(private http: HttpClient) { 

  }

  login(data: any): Observable<any> {
    return this.http.post(this.loginPath, data)
  }

  register(data: any) : Observable<any> {
    return this.http.post(this.registerPath, data)
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('currentUsername');
  }

  getCurrentUser() {
    return this.http.get<User>(this.currentUserPath)
  }

  getUserById(userId: string | null) {
    return this.http.get<User>(this.getUserByIdPath + userId)
  }

  saveToken(token: string) {
    localStorage.setItem('token', token)
  }

  getToken() {
    return localStorage.getItem('token')
  }

  saveCurrentUsername(username: string) {
    localStorage.setItem('currentUsername', username);
  }

  getCurrentUsername() {
    return localStorage.getItem('currentUsername')
  }

  isAuthenticated() {
    if(this.getToken())
      return true;
    return false;
  }
}
