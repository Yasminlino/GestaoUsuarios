import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  getToken(): string | null {
    return localStorage.getItem('token');  
  }

  logout(): void {
    localStorage.removeItem('token'); 
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (token) {
      return true;
    }
    return false;
  }
}
