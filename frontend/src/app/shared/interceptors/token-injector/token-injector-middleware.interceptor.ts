import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

export const tokenInjectorMiddlewareInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = inject(AuthService).getAuthToken();

  req = req.clone({
    headers: req.headers.append('Authorization',authToken)
  });

  return next(req);
};
