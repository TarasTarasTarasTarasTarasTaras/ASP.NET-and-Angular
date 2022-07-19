import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errorMessage: string = '';
  
  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private router: Router
    ) { 
    this.registerForm = this.fb.group({
      'email': ['', [Validators.required, Validators.email]],
      'name': ['', [Validators.required]],
      'userName': ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      'password': ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]],
    }, { validator: this.comparePasswords })
  }

  ngOnInit(): void {
  }

  register() {
    this.authService.register(this.registerForm.value).subscribe(data => {
      console.log(this.registerForm.value)
      
      this.authService.login(this.registerForm.value).subscribe(data => {
        console.log(this.registerForm.value)
        this.authService.saveToken(data['token']);
        this.authService.saveCurrentUsername(this.registerForm.value['userName']);

        this.router.navigate([''])
      })
    },
    (error) => {
      this.errorMessage = error.errorMessage
    })
  }

  comparePasswords(fb: FormGroup) {
    let confirmPassword = fb.get('confirmPassword');

    if(confirmPassword?.errors == null || 'passwordMismatch' in confirmPassword.errors) {
      if(fb.get('password')?.value != confirmPassword?.value) {
        confirmPassword?.setErrors({ passwordMismatch: true });
      }
      else
        confirmPassword?.setErrors(null);
    }
    else 
      confirmPassword?.setErrors(null);
  }
}
