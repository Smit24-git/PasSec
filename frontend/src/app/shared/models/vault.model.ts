export interface CreateVaultRequest {
    vaultName:string;
    description?:string;
    useUserKey:boolean;
    userKey?:boolean;
    Keys: {
        email?:string;
        username?:string;
        password:string;
        accessLocation?:string;
        securityQuestions?:{}[];
    }
}

export interface CreateVaultStorageKeyRequest {
    userKey?: string,
    vaultId: string,
    keyName: string,
    username?: string,
    password: string,
    email?: string,
    accessLocation?: string,
    securityQAs?: {}[],
}

export interface ListUserVaultResponse {
    vaults:Vault[];
}

export interface Vault {
    vaultId:string;
    vaultName:string;
    description?:string;
    storageKeys: VaultStorageKey[]
}

export interface VaultStorageKey {
    keyName:string;
    emailAddress?:string;
    username?:string;
    password:string;
    accessLocation?:string;
    securityQuestions?:{}[];

    unmaskPassword: boolean;
}