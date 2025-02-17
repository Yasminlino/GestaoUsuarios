import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { catchError } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UsuariosService {
    private apiUrl = 'http://localhost:5191/';

    constructor(private http: HttpClient) { }

    buscarUsuarios(): Observable<any> {
        const token = localStorage.getItem('token');

        if (!token) {
            throw new Error('Token não encontrado');
        }

        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        });

        return this.http.get(this.apiUrl + 'GetUsuarios', { headers: headers }).pipe(
            catchError(error => {
                throw error.error || 'Erro ao buscar usuários';
            })
        );
    }

    editarUsuarios(usuario: any, loginAntigo: string): Observable<any> {
        const token = localStorage.getItem('token');

        if (!token) {
            throw new Error('Token não encontrado');
        }

        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        });
        const body = {
            "login": usuario.login,
            "senha": usuario.senha,
            "nomeUsuario": usuario.nomeUsuario,
            "role": "Admin"
        };

        return this.http.put(this.apiUrl + `AlterarDadosUsuario/${loginAntigo}`, body, { headers: headers }).pipe(
            tap(response => {
                console.log('Usuário atualizado com sucesso', response);
            }),
            catchError(error => {
                throw error.error || 'Erro ao atualizar usuários';
            })
        );
    }

    cadastrarUsuarios(usuario: any): Observable<any> {

        const body = {
            "login": usuario.login,
            "senha": usuario.senha,
            "nomeUsuario": usuario.nomeUsuario,
            "role": "Admin"
        };

        return this.http.post(this.apiUrl + "CadastrarUsuario", body).pipe(
            tap(response => {
                console.log('Usuário cadastrado com sucesso', response);
            }),
            catchError(error => {
                throw error.error || 'Erro ao cadastrar usuários';
            })
        );
    }

    deletarUsuario(idUsuario: any): Observable<any> {
        const token = localStorage.getItem('token');

        if (!token) {
            throw new Error('Token não encontrado');
        }

        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        });

        return this.http.delete(this.apiUrl + `DeletarUsuario/${idUsuario}`, { headers: headers }).pipe(
            tap(response => {
                console.log('Usuário cadastrado com sucesso', response);
            }),
            catchError(error => {
                throw error.error || 'Erro ao cadastrar usuários';
            })
        );
    }

}