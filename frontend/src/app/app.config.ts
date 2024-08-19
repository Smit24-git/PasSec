import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { exceptionHandlerMiddlewareInterceptor } from './shared/interceptors/exception-handler/exception-handler-middleware.interceptor';
import { tokenInjectorMiddlewareInterceptor } from './shared/interceptors/token-injector/token-injector-middleware.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        tokenInjectorMiddlewareInterceptor,
        exceptionHandlerMiddlewareInterceptor,
      ])
    ),
    provideAnimationsAsync(),
  ]
};
