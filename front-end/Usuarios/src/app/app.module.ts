import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms'; 
import { LoginComponent } from './auth/login/login.component';import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { HttpClientModule } from '@angular/common/http';
import { UsuariosComponent } from './pages/usuarios/usuarios/usuarios.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatIconModule } from '@angular/material/icon';
import { ModalDadosUsuarioComponent } from './pages/Modais/modal-dados-usuario/modal-dados-usuario.component'
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { ModalCadastroUsuarioComponent } from './pages/Modais/modal-cadastro-usuario/modal-cadastro-usuario.component';

import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthGuard } from './auth.guard';
import { AuthService } from './Services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UsuariosComponent,
    ModalDadosUsuarioComponent,
    ModalCadastroUsuarioComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes) ,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatIconModule,
    FormsModule,
    MatButtonModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    AuthInterceptor

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
