import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private testUrl = environment.passecApi + '/test'

  private http = inject(HttpClient);
  constructor() { }

  public runTest(): Observable<void> {
    return this.http.get<void>(this.testUrl);
  }
}
