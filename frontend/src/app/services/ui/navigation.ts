import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({providedIn: "root"})
export class ApplicationRouter{
  constructor(public readonly router: Router) {

  }

  public async RedirectToHome(): Promise<void>{
    await this.router.navigateByUrl('/');
  }

  public async RedirectToTaskProfile(id: number): Promise<void>{
    await this.router.navigate(['/tasks', id])
  }
}
