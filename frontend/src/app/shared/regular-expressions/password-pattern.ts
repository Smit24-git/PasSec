
export const PasswordPattern = new RegExp("(?=.*[^a-zA-Z0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?!.*[\\s])([a-zA-Z0-9]|[^a-zA-Z0-9]){16,}");