import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as bootstrap from 'bootstrap';
import { UsuariosService } from 'src/app/Services/usuarios.service';
import { Router } from '@angular/router';
import { UsuariosComponent } from '../../usuarios/usuarios/usuarios.component';

@Component({
  selector: 'app-modal-dados-usuario',
  templateUrl: './modal-dados-usuario.component.html'
})
export class ModalDadosUsuarioComponent implements OnInit, AfterViewInit {
  formularioAtualizarUsuario: FormGroup;
  expandirFormulario: boolean = false;
  senhaValida: boolean = true;
  senhaVisivel: boolean = false;
  senha: string = '';
  dadosAntigos: any = {}; 
  
  @ViewChild('editModal') modalElement!: ElementRef;
  private modal!: bootstrap.Modal;
  atualizarUsuariosEvent: any;

  constructor(
    private formBuilder: FormBuilder,
    private usuarioService: UsuariosService,
    private router: Router,
    private usuarioComponent: UsuariosComponent
  ) {
    this.formularioAtualizarUsuario = this.formBuilder.group({
      login: ['', [Validators.required]],
      nomeUsuario: ['', [Validators.required]],
      senha: ['', []]
    });
  }

  ngOnInit(): void {}

  toggleSenha(): void {
    this.expandirFormulario = !this.expandirFormulario;
  }
  
  visualisarSenha(): void {
    this.senhaVisivel = !this.senhaVisivel;
  }

  ngAfterViewInit() {
    this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
  }

  open(usuario: any) {
    this.dadosAntigos = { ...usuario };  
    this.modal.show();
    
    this.formularioAtualizarUsuario.patchValue({
      login: usuario.login,
      nomeUsuario: usuario.nomeUsuario
    });
    
    if (this.modalElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
  }

  submitAtualizarUsuario() {
    if (this.formularioAtualizarUsuario.dirty && (this.senhaValida || !this.expandirFormulario)) {
      const loginData = this.formularioAtualizarUsuario.value;

      if (this.expandirFormulario && this.senha) {
        loginData.senha = this.senha;
      }

      const dadosParaEnviar = { 
        ...this.dadosAntigos,  
        ...loginData      
      };
      

      if(!loginData.senha)
        dadosParaEnviar.senha = "null"

      const loginAntigo = this.dadosAntigos.id;
      this.usuarioService.editarUsuarios(dadosParaEnviar, loginAntigo).subscribe({
        next: (response) => {
          console.log('Dados atualizados com sucesso', response);
          if (response.token) {
            localStorage.setItem('token', response.token);
          }
          this.usuarioComponent.submitBuscarUsuarios();
          this.close()
        },
        error: (error) => {
          console.error('Erro ao atualizar dados do usuário', error);
        }
      });
      this.usuarioComponent.menssagemSucesso("Usuario atualizado com sucesso!");
    } else {
      console.error('Formulário ou senha inválidos');
    }
  }

  close() {
    if (this.modal) {
      this.modal.hide(); 
    }

    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        backdrop.remove();
    }

    this.formularioAtualizarUsuario.reset();
    this.expandirFormulario = false;
    this.senha = '';
    this.senhaValida = true;
  }
}