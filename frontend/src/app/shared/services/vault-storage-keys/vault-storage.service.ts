import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CreateVaultStorageKeyRequest, UpdateVaultStorageKeyRequest } from '../../models/vault.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VaultStorageService {

  private storageKey =  environment.passecApi + '/vault-storage-security-keys';

  private http = inject(HttpClient);

  constructor() { }

  public AddNewKey(req:CreateVaultStorageKeyRequest):Observable<void>{
    return this.http.post<void>(this.storageKey, req);
  }
  
  public updateKey(securityKeyId:string, req:UpdateVaultStorageKeyRequest):Observable<void>{
    return this.http.put<void>(this.storageKey+'/'+securityKeyId, req);
  }
  
  
}
