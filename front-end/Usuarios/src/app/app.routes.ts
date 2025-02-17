import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { UsuariosComponent } from './pages/usuarios/usuarios/usuarios.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'login'
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'usuarios',
        component: UsuariosComponent,
        canActivate: [AuthGuard]
    }
];
