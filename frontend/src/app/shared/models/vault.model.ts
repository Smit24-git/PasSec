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

export interface Vault{

}