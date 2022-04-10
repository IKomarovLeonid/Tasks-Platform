import {Injectable} from "@angular/core";
import {
  AffectionViewModel,
  CreateTaskRequestModel,
  TasksApi,
  TaskViewModel,
  VisibleScope
} from "../../communication/main.api";
import {SelectResult} from "../queries/select.result";
import {lastValueFrom} from "rxjs";
import {CommandResult} from "../queries/command.result";

@Injectable({providedIn: "root"})
export class TasksMediator{
  constructor(private api: TasksApi) {
  }


  async GetAll(scope: VisibleScope): Promise<SelectResult<TaskViewModel>>{
    try{
      const response$ = await this.api.get(scope);
      const model = await lastValueFrom(response$);
      return new SelectResult<TaskViewModel>(model.items !!);
    }
    catch (e){
      // @ts-ignore
      return new SelectResult<TaskViewModel>([], e);
    }
  }

  async CreateAsync(model: CreateTaskRequestModel): Promise<CommandResult<AffectionViewModel>>{
    try {
      const response$ = await this.api.create(model);
      const data = await lastValueFrom(response$);
      return new CommandResult<AffectionViewModel>(data);
    }
    catch (error){
      // @ts-ignore
      return new CommandResult<AffectionViewModel>(undefined, error.response);
    }
  }

  async ArchiveAsync(id: number): Promise<CommandResult<AffectionViewModel>>{
    try {
      const response$ = await this.api.archive(id);
      const data = await lastValueFrom(response$);
      return new CommandResult<AffectionViewModel>(data);
    }
    catch (error){
      // @ts-ignore
      return new CommandResult<AffectionViewModel>(undefined, error.response);
    }
  }
}
