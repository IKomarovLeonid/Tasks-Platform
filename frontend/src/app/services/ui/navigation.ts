import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({providedIn: "root"})
export class ApplicationRouter{
  constructor(public readonly router: Router) {

  }

  public async RedirectToTasks(): Promise<void>{
    await this.router.navigateByUrl('/tasks');
  }
}
