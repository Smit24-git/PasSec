import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CreateVaultKeyQuestionRequest, UpdateVaultKeyQuestionRequest } from '../../models/vault.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VaultKeyQAService {
  private qaUrl = environment.passecApi + '/security-key-qas'
  
  private http = inject(HttpClient);
  constructor() { }

  addNewQuestion(req: CreateVaultKeyQuestionRequest):Observable<void> {
    return this.http.post<void>(this.qaUrl,req);
  }

  updateQuestion(id:string, req: UpdateVaultKeyQuestionRequest):Observable<void> {
    return this.http.put<void>(`${this.qaUrl}/${id}`,req);
  }
}
