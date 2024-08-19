export interface RegisterUserRequest {
    userName:string;
    password:string;
}

export interface LoginUserRequest {
    userName:string;
    password:string;
}

export interface LoginUserResponse {
    token:string;
    userName:string;
}