import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CreateVaultRequest, ListUserVaultResponse, Vault } from '../../models/vault.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VaultService {

  private vaultURL = environment.passecApi+'/vaults';

  private http = inject(HttpClient);
  constructor() { }

  public createVault(req:CreateVaultRequest):Observable<Vault>{
      return this.http.post<Vault>(this.vaultURL, req);
  }

  public listUserVaults():Observable<ListUserVaultResponse>{
      return this.http.get<ListUserVaultResponse>(this.vaultURL);
  }
}
