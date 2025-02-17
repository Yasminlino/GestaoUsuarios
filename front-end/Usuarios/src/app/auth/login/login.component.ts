import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/Services/login.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  formularioLogin!: FormGroup
  errorMessage: any;
  senhaVisivel: boolean = false;

  constructor(private formBuilder: FormBuilder,private authService: AuthService, private loginService: LoginService, private router: Router) {
  }

  toggleSenha(): void {
    this.senhaVisivel = !this.senhaVisivel;
  }

  ngOnInit(): void {
    this.formularioLogin = this.formBuilder.group({
      login: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }
  
  submitLogin() {
    if (this.formularioLogin.invalid) return;

    const loginData = this.formularioLogin.value;

    this.loginService.LoginUsuario(loginData).subscribe({
      next: (response) => {
        console.log('Login bem-sucedido', response);
        if(response.token){
          localStorage.setItem('token', response.token);
            this.router.navigate(['/usuarios']);
        }
      },
      error: (error) => {
        this.errorMessage = error;  
      }
    });
  }

  logout()
  {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
