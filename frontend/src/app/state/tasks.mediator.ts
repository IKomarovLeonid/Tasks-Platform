import {Injectable} from "@angular/core";
import {TasksApi, TaskViewModel, VisibleScope} from "../../communication/main.api";
import {SelectResult} from "../queries/select.result";
import {lastValueFrom, Observable} from "rxjs";

@Injectable({providedIn: "root"})
export class TasksMediator{
  constructor(private api: TasksApi) {
  }


  async GetAll(scope: VisibleScope): Promise<SelectResult<TaskViewModel>>{
    const response$ = await this.api.get(scope);
    const model = await lastValueFrom(response$);
    return new SelectResult<TaskViewModel>(model.items !!);
  }
}
