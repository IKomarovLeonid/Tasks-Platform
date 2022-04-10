import {Injectable} from "@angular/core";
import {AffectionViewModel, JobSettings, SettingsApi} from "../../communication/main.api";
import {lastValueFrom} from "rxjs";
import {CommandResult} from "../queries/command.result";

@Injectable({providedIn: "root"})
export class SettingsMediator{
  constructor(private api: SettingsApi) {

  }

  async GetJobsAsync(): Promise<CommandResult<JobSettings>>{
    try{
      const response = this.api.getJobSettings();
      const data = await lastValueFrom(response);
      return new CommandResult<JobSettings>(data);
    }
    catch (e){
      // @ts-ignore
      return new CommandResult<AffectionViewModel>(undefined, error.response);
    }
  }

  async SetJobsAsync(jobs: JobSettings): Promise<CommandResult<AffectionViewModel>>{
    try{
      const response = this.api.setJobSettings(jobs);
      const data = await lastValueFrom(response);
      return new CommandResult<AffectionViewModel>(data);
    }
    catch (e){
      // @ts-ignore
      return new CommandResult<AffectionViewModel>(undefined, error.response);
    }
  }
}
