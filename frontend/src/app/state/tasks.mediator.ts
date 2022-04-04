import {Injectable} from "@angular/core";
import {TasksApi} from "../../communication/main.api";

@Injectable({providedIn: "root"})
export class TasksMediator{
  constructor(private api: TasksApi) {
  }

}
