import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDadosUsuarioComponent } from './modal-dados-usuario.component';

describe('ModalDadosUsuarioComponent', () => {
  let component: ModalDadosUsuarioComponent;
  let fixture: ComponentFixture<ModalDadosUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDadosUsuarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalDadosUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
