import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { ModalWindowComponent } from './modal-window/modal-window.component';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  postForm: FormGroup;
  modal = true;
  user!: User;

  constructor(
    private fb: FormBuilder, 
    private postService: PostService,
    private authService: AuthService,
    private router: Router,) 
  { 
    this.postForm = this.fb.group({
      imageUrl: [''],
      description: ['']
    })

    this.authService.getCurrentUser().subscribe(res => {
      this.user = res
    })
  }

  ngOnInit(): void {
  }

  create() {
    this.postService.create(this.postForm.value).subscribe(res => {
      this.router.navigate([''])
    })
  }

  edit() {
    this.modal = true;
  }

  get imageUrl() {
    return this.postForm.get('imageUrl');
  }

  get description() {
    return this.postForm.get('description');
  }

}
