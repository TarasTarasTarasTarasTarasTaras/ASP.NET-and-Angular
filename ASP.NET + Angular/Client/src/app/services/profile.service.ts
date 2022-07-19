import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Profile } from '../models/profile';
import { Subscribe } from '../models/subscribe';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private getProfilePath = environment.apiUrl + 'profiles/profile/';
  private editProfilePath = environment.apiUrl + 'profiles/edit/';
  private getSubscribesPath = environment.apiUrl + 'profiles/subscribes/';
  private postSubscribePath = environment.apiUrl + 'profiles/subscribe/'

  constructor(private http: HttpClient) { }

  getProfileById(userId: string): Observable<Profile> {
    return this.http.get<Profile>(this.getProfilePath + userId);
  }

  edit(data: any): Observable<Profile> {
    return this.http.put<Profile>(this.editProfilePath, data);
  }

  getUserSubscribes(userId: string): Observable<Array<Subscribe>> {
    return this.http.get<Array<Subscribe>>(this.getSubscribesPath + userId);
  }

  subscribe(userId: string): Observable<Subscribe> {
    return this.http.post<Subscribe>(this.postSubscribePath + userId, null)
  }
}
