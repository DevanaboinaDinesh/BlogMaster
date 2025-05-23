import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';
// import {k jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
   const cookieService = inject(CookieService);
   const authService = inject(AuthService);
   const router = inject(Router);
   const user = authService.getUser();
   let token=cookieService.get('Authorization');
   if(token && user){
      token = token.replace('Bearer ', '');
      const decodedToken = jwtDecode(token);
      const expirationDate = decodedToken.exp? decodedToken.exp* 1000 : new Date().getTime();
      const currentTime = new Date().getTime();
      if(expirationDate <currentTime)
      {
        authService.logout();
        return router.createUrlTree(['/login'], {queryParams : {returnUrl : state.url}});
      }
      else
      {
        if(user.roles.includes('WRITER'))
        {
          return true;
        }
        else
        {
          alert("Unauthorized");
          return false;
        }
      }
   }
   else
   {
    authService.logout();
    return router.createUrlTree(['/login'], {queryParams : {returnUrl : state.url}});
   }
  // Check the JWT Token
};
