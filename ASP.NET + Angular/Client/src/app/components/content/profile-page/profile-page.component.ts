import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscriber } from 'rxjs';
import { Subscribe } from 'src/app/models/subscribe';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  user!: User;
  currentUser!: User;
  subscribes!: Array<Subscribe>

  constructor(
    private route: ActivatedRoute, 
    private authService: AuthService,
    private router: Router,
    private profileService: ProfileService
    ) { 
    this.authService.getUserById(this.route.snapshot.paramMap.get('userId')).subscribe(res => {
      this.user = res

      this.loadSubscribes()
    })

    this.authService.getCurrentUser().subscribe(res => {
      this.currentUser = res
    })
  }

  ngOnInit(): void {
  }

  userPressed(userId: string) {
    this.authService.getUserById(userId).subscribe(res => {
      this.user = res
      this.loadSubscribes()
      this.router.navigate([`profile/${userId}`])
    })
  }

  loadSubscribes() {
    this.profileService.getUserSubscribes(this.user.id).subscribe(res => {
      this.subscribes = res
      this.user.followers = res.filter(s => s.followerUserId == this.user.id)
      this.user.subscribers = res.filter(s => s.subscriberUserId == this.user.id)
    })
  }

  getCountExistingPosts() {
    var existingPosts = this.user.posts.filter(p => !p.isDeleted)
    return existingPosts.length
  }

  subscribe() {
    this.profileService.subscribe(this.user.id).subscribe(res => {
      this.loadSubscribes()
    })
  }

  isFollower() {
    if(this.user.subscribers.find(s => s.followerUserId == this.currentUser.id) != undefined) {
      return true;
    }
    return false;
  }

  profileIsPublicOrFollow() {
    if(this.user.id == this.currentUser.id || !this.user.profile.isPrivate)
      return true;

    return this.isFollower()
  }
}
