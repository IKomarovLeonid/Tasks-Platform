import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class UiNotificationService{
  constructor() {
  }

  public onError(response: string){
    const model = JSON.parse(response);
    alert(model.message);
  }
}
