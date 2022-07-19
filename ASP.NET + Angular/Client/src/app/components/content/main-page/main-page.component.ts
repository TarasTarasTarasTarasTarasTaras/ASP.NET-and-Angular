import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  posts!: Array<Post>;
  user!: User;

  constructor(
    private authService: AuthService,
    private postService: PostService,
    private router: Router
    ) 
    { 
      this.authService.getCurrentUser().subscribe(res => {
        this.user = res
      })
    }

  ngOnInit(): void {
    this.loadPosts()
  }
  
  logout() {
    this.authService.logout();
    this.router.navigate(['login'])
  }

  loadPosts() {
    this.postService.getAll().subscribe(posts => {
      this.posts = posts
    })
  }

  createPost() {
    this.router.navigate(['createpost'])
  }

  likePressed(postId: number) {
    this.postService.likePressed(postId).subscribe(res => {

    })
    this.loadPosts()
  }

  savePressed(postId: number) {
    this.postService.savePressed(postId).subscribe(res => {

    })
    this.loadPosts()
  }

  postIsLiked(post: Post): boolean {
    var isLiked = post.likes.find((l) => l.userId === this.user.id)

    if(isLiked === undefined || isLiked === null)
      return false;
    return true;
  }

  postIsSaved(post: Post): boolean {
    var isSaved = post.saves.find((l) => l.userId === this.user.id)

    if(isSaved === undefined)
      return false;
    return true;
  }

  userPressed() {
    this.router.navigate(['profile/' + this.user.id])
  }
}
