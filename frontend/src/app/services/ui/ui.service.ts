import {Injectable} from "@angular/core";
import {Notifications} from "./notifications";
import {ErrorParser} from "./error.parser";
import {ApplicationRouter} from "./navigation";

@Injectable({providedIn: "root"})
export class UiService{
  constructor(
    public readonly notifications: Notifications,
    public readonly parser: ErrorParser,
    public readonly router: ApplicationRouter) {
  }

}
