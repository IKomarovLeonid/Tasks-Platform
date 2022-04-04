import {Injectable} from "@angular/core";
import {SettingsApi} from "../../communication/main.api";

@Injectable({providedIn: "root"})
export class SettingsMediator{
  constructor(private api: SettingsApi) {

  }
}
