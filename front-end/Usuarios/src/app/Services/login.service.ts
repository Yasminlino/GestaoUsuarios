import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root' 
})
export class LoginService {

  private apiUrl = 'http://localhost:5191/Autenticar';

  constructor(private http: HttpClient) { }

    LoginUsuario(dadosLogin: { login: string, senha: string }): Observable<any> {
    return this.http.post(this.apiUrl, dadosLogin).pipe(  
      catchError(error => {
        throw error.error.message || 'Erro desconhecido';
      })
    );
  }
}
