import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { LoginUserRequest, LoginUserResponse, RegisterUserRequest } from '../../models/user.model';
import { Observable, Subject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private registerUrl = environment.passecApi + '/users/register';
  private loginUrl = environment.passecApi + '/users/login';
  private $triggerLogin = new Subject();

  triggerLoginObserver = this.$triggerLogin.asObservable();

  private http = inject(HttpClient);
  constructor() { }

  /**
   * checks if token exists. this does not take an accont of the expiration of the token.
   * @returns boolean flag true if logged in
   */
  public isLoggedIn() {
    let token = this.getAuthToken();
    return !!token; //assumes logged in if token exists.
  }

  public registerUser(req:RegisterUserRequest):Observable<any> {
    return this.http.post(this.registerUrl, req);
  }

  public loginUser(req:LoginUserRequest):Observable<LoginUserResponse> {
    return this.http.post<LoginUserResponse>(this.loginUrl, req).pipe(tap((res)=>{
      localStorage.setItem('token',res.token);
      localStorage.setItem('user',res.userName);
      return res;
    }));
  }

  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  public getAuthToken():string{
    const token = localStorage.getItem('token');
    if(!token) return '';

    return "Bearer " + token;
  }

  public getLoggedInUserName():string | null {
    return localStorage.getItem('user');
  }

  public triggerLogin() {
    this.$triggerLogin.next({});
  }
}
