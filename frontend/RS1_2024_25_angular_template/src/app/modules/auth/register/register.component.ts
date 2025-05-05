import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MyAuthService, RegisterRequest } from '../../../services/auth-services/my-auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  standalone: false
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private router: Router,
    private authService: MyAuthService,
    private fb: FormBuilder,
    private translate: TranslateService
  ) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required]
    });
  }

  onRegister() {
    if (this.registerForm.invalid) return;

    const registerData: RegisterRequest = this.registerForm.value;

    this.authService.register(registerData).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
        this.router.navigateByUrl('/auth/login');
      },
      error: (error) => {
        // Pokušaj prevesti poruku ako postoji ključ
        this.translate.get('REGISTER.ERROR').subscribe(translated => {
          this.errorMessage = translated || error.message;
        });
      }
    });
  }
}
