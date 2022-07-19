import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private router: Router
    ) { 
      this.loginForm = this.fb.group({
        'userName': ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
        'password': ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]]
      })

    }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe(data => {
      this.authService.saveToken(data['token']);
      this.authService.saveCurrentUsername(this.loginForm.value['userName']);
      this.router.navigate(['']);

      console.log(data.errorMessage)
    },
    (error) => {
      this.errorMessage = error.errorMessage
    })
  }
}
