import { OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as bootstrap from 'bootstrap';
import { UsuariosService } from 'src/app/Services/usuarios.service';
import { Router } from '@angular/router';
import { UsuariosComponent } from '../../usuarios/usuarios/usuarios.component';

@Component({
  selector: 'app-modal-cadastro-usuario',
  templateUrl: './modal-cadastro-usuario.component.html'
})
export class ModalCadastroUsuarioComponent implements OnInit, AfterViewInit {
  formularioCadastrarUsuario: FormGroup;
  expandirFormulario: boolean = false;
  senhaValida: boolean = true;
  dadosAntigos: any = {};
  errorMessage: any;
  senhaVisivel: boolean = false;

  @ViewChild('cadastroModal') modalElement!: ElementRef;
  private modal!: bootstrap.Modal;

  constructor(
    private formBuilder: FormBuilder,
    private usuarioService: UsuariosService,
    private router: Router,
    private usuariosComponent: UsuariosComponent
  ) {
    this.formularioCadastrarUsuario = this.formBuilder.group({
      login: ['', [Validators.required]],
      nomeUsuario: ['', [Validators.required]],
      senha: ['', [Validators.required]]
    });
  }

  ngOnInit(): void { }

  toggleSenha(): void {
    this.expandirFormulario = !this.expandirFormulario;
  }

  visualisarSenha(): void {
    this.senhaVisivel = !this.senhaVisivel;
  }


  ngAfterViewInit() {
    this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
  }

  open() {
    this.formularioCadastrarUsuario.reset({
      login: '',
      nomeUsuario: '',
      senha: ''
    })

    this.modal.show();
  }

  validarSenha(senha: string): boolean {
    const regexComprimento = /.{8,}/;

    const regexMaiuscula = /[A-Z]/;
    const regexMinuscula = /[a-z]/;
    const regexNumero = /[0-9]/;
    const regexEspecial = /[!@#$%^&*(),.?":{}|<>]/;

    if (
      regexComprimento.test(senha) &&
      regexMaiuscula.test(senha) &&
      regexMinuscula.test(senha) &&
      regexNumero.test(senha) &&
      regexEspecial.test(senha)
    ) {
      return true;
    } else {
      this.menssagemErro("Senha inválida!");
      return false;
    }
  }

  menssagemErro(message: string) {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000);
  }


  submitCadastrarUsuario() {
    const senhaValida = this.validarSenha(this.formularioCadastrarUsuario.value.senha);
    if (this.formularioCadastrarUsuario.valid && senhaValida) {
      const dadosCadastro = this.formularioCadastrarUsuario.value;

      this.usuarioService.cadastrarUsuarios(dadosCadastro).subscribe({
        next: (response) => {
          console.log('Dados atualizados com sucesso', response);
          if (response.token) {
            localStorage.setItem('token', response.token);
          }
          this.modal.hide();
        },
        error: (error) => {
          this.usuariosComponent.menssagemErro(error.error);
          console.error('Erro ao atualizar dados do usuário', error);
          return false;
        }
      });
      this.usuariosComponent.menssagemSucesso('Cadastro realizado com sucesso!');
    } else {
      console.error('Formulário ou senha inválidos');
    }
  }

  closeCadastro() {
    if (this.modal) {
      this.modal.hide();
    }

    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }

    this.formularioCadastrarUsuario.reset();
    this.expandirFormulario = false;
    this.senhaValida = true;
    this.errorMessage = "";
    this.senhaVisivel = false;

  }
}