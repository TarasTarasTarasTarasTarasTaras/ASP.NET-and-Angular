import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  currentUser!: User;
  profileForm!: FormGroup;
  errorMessage!: string;

  constructor(
    private authService: AuthService, 
    private fb: FormBuilder, 
    private router: Router,
    private profileService: ProfileService
    ) { 
    this.authService.getCurrentUser().subscribe(user => {
      this.currentUser = user

      this.profileForm = this.fb.group({
      'firstName': [this.currentUser.profile.firstName],
      'lastName': [this.currentUser.profile.lastName],
      'country': [this.currentUser.profile.country],
      'city': [this.currentUser.profile.city],
      'mainPhotoUrl': [this.currentUser.profile.mainPhotoUrl],
      'biography': [this.currentUser.profile.biography],
      'isPrivate': [this.currentUser.profile.isPrivate]
      })

      console.log(user)
    })
  }

  ngOnInit(): void {
  }

  userPressed(userId: string) {
    this.router.navigate([`profile/${userId}`])
  }

  editProfile() {
    this.profileService.edit(this.profileForm.value).subscribe(res => {
      this.router.navigate([`profile/${this.currentUser.id}`])
    })
  }
}
