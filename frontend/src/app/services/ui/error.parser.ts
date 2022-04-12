import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class ErrorParser{
  constructor() {
  }

  Parse(response: string): string {
    try{
      const model = JSON.parse(response);
      return model.message === "" ? model.errorCode : model.message;
    }
    catch (e){
      // return as unknown error
      return response;
    }
  }
}
