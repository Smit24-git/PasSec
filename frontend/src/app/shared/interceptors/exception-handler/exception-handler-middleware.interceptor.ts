import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { tap } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';
import { MessageService } from 'primeng/api';
import { NotificationService } from '../../services/notification/notification.service';

export const exceptionHandlerMiddlewareInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const messageService = inject(NotificationService);

  return next(req).pipe(tap({
    next: ()=>{
      
    },
    error: (e)=>{
      console.log(e);
      switch(e.status){
        case 401:
          authService.logout();
          authService.triggerLogin();
          break;
        case 403:
          authService.logout();
          authService.triggerLogin();
          break;
      }
      const errors = e.error?.Errors as string[] | null | undefined;
      if(errors)
        errors.forEach((errm:string)=>{
          messageService.showMessage({severity: 'error', summary: errm});
        });
      else
        messageService.showMessage({severity: 'error', summary: 'something went wrong. please try again later.'})
    }
  }));
};
