import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import Auth from '@aws-amplify/auth';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StandardAuthGuard implements CanActivate {

  /**
   *
   */
  constructor(private router: Router) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return new Promise ((resolve, reject)=>{
        Auth.currentAuthenticatedUser({
          bypassCache: false
        })
        .then((user)=>{
          if(user){
            resolve(true);
          }
          else
            reject("User Unable to resolve");
        })
        .catch(() => {
          this.router.navigate(['/login']);
          resolve(false);
        });
    })
  }

}
