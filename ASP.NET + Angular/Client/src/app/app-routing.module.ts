import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreatePostComponent } from './components/content/create-post/create-post.component';
import { MainPageComponent } from './components/content/main-page/main-page.component';
import { ProfileEditComponent } from './components/content/profile-edit/profile-edit.component';
import { ProfilePageComponent } from './components/content/profile-page/profile-page.component';
import { LoginComponent } from './components/identity/login/login.component';
import { RegisterComponent } from './components/identity/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  { path: '', component: MainPageComponent, canActivate: [AuthGuardService] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'createpost', component: CreatePostComponent, canActivate: [AuthGuardService] },
  { path: 'profile/edit', component: ProfileEditComponent },
  { path: 'profile/:userId', component: ProfilePageComponent, canActivate: [AuthGuardService] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
