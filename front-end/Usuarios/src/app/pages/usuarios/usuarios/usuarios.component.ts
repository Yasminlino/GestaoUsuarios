import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import * as bootstrap from 'bootstrap';
import { UsuariosService } from 'src/app/Services/usuarios.service';
import { ModalDadosUsuarioComponent } from '../../Modais/modal-dados-usuario/modal-dados-usuario.component';
import { ModalCadastroUsuarioComponent } from 'src/app/pages/Modais/modal-cadastro-usuario/modal-cadastro-usuario.component';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html'
})
export class UsuariosComponent implements OnInit {
  usuarios: any[] = [];
  errorMessage: any;
  successMessage: string = '';

  constructor(private usuariosService: UsuariosService, private router: Router) { }

  ngOnInit(): void {
    this.submitBuscarUsuarios();
  }

  submitBuscarUsuarios() {
    this.usuariosService.buscarUsuarios().subscribe({
      next: (response) => {
        console.log('Usuários carregados com sucesso', response);
        this.usuarios = response;
      },
      error: (error) => {
        this.errorMessage = error;
        console.error('Erro ao carregar usuários', error);
      }
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  menssagemSucesso(message: string) {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = '';
    }, 5000);
  }

  menssagemErro(message: string) {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000);
  }

  deleteUsuario(usuario: any) {
    const confirmDelete = window.confirm('Você tem certeza que deseja deletar este usuário?');

    if (confirmDelete) {
      this.usuariosService.deletarUsuario(usuario).subscribe({
        next: (response) => {
          this.menssagemSucesso('Usuario deletado com sucesso');
          console.log('Usuario deletado com sucesso', response);
          this.usuarios = response;
          this.submitBuscarUsuarios();
        },
        error: (error) => {
          this.menssagemErro(error.message);
          console.error('Erro ao deletar usuários', error);
        }
      });
    }
  }
}
