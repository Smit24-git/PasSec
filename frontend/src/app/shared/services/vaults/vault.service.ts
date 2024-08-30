import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { map, Observable, tap } from 'rxjs';
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

  public getVault(vaultId:string, vaultReq: {vaultId:string, userKey?: string} ):Observable<Vault> {
    return this.http.post<{vault:Vault}>(this.vaultURL+"/"+vaultId,vaultReq ).pipe(map(res=>res.vault))
  }
}
