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

export interface ListUserVaultResponse {
    vaults:Vault[];
}

export interface Vault{
    vaultName:string;
    description?:string;
}