import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private getAllPostsPath = environment.apiUrl + 'post/allexistingposts';
  private createPostPath = environment.apiUrl + 'post/create/';
  private likePressedPath = environment.apiUrl + 'post/like/';
  private savePressedPath = environment.apiUrl + 'post/save/';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Array<Post>> {
    return this.http.get<Array<Post>>(this.getAllPostsPath);
  }

  create(data: any): Observable<Post> {
    return this.http.post<Post>(this.createPostPath, data);
  }

  likePressed(postId: number): Observable<any> {
    return this.http.post<any>(this.likePressedPath + postId, null);
  }

  savePressed(postId: number): Observable<any> {
    return this.http.post<any>(this.savePressedPath + postId, null);
  }
}
