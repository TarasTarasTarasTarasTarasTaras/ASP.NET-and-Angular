import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/identity/login/login.component';
import { RegisterComponent } from './components/identity/register/register.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MainPageComponent } from './components/content/main-page/main-page.component';
import { ErrorInterceptorService } from './services/error-interceptor.service';
import { PostService } from './services/post.service';
import { CreatePostComponent } from './components/content/create-post/create-post.component';
import { ModalWindowComponent } from './components/content/create-post/modal-window/modal-window.component';
import { ProfilePageComponent } from './components/content/profile-page/profile-page.component';
import { ProfileService } from './services/profile.service';
import { ProfileEditComponent } from './components/content/profile-edit/profile-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainPageComponent,
    CreatePostComponent,
    ModalWindowComponent,
    ProfilePageComponent,
    ProfileEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    
  ],
  providers: [
    AuthService,
    PostService,
    ProfileService,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
