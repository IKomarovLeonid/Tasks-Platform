import {Component, OnInit} from "@angular/core";
import {SettingsMediator} from "../../../state/settings.mediator";
import {JobSettings} from "../../../../communication/main.api";
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {MatDialogRef} from "@angular/material/dialog";
import {UiService} from "../../../services/ui/ui.service";

@Component({
  selector: 'jobs-view-component',
  templateUrl: 'jobs-view.component.html',
})
export class JobsViewComponent implements OnInit{

  form = new FormGroup({});
  model = new JobSettings();
  fields: FormlyFieldConfig[] = [
    {
      key: 'checkTaskExpirationJobSec',
      type: 'input',
      templateOptions: {
        label: 'Check tasks expiration period (seconds)',
        type: 'number',
        min: 0,
        max: 80400,
        required: true,
      }
    },
    {
      key: 'reloadCachesJobSec',
      type: 'input',
      templateOptions: {
        label: 'Reload caches period (seconds)',
        type: 'number',
        min: 0,
        max: 80400,
        required: true,
      }
    }
  ];

  constructor(
    private mediator: SettingsMediator,
    public dialogRef: MatDialogRef<JobsViewComponent>,
    public readonly ui: UiService) {

  }

  async ngOnInit(): Promise<void> {
    const response = await this.mediator.GetJobsAsync();
    if(response.isSuccess()) this.model = response.data!!;
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
    }
  }

  async onSubmit(): Promise<void>{
    const response = await this.mediator.SetJobsAsync(this.model);
    if(response.isSuccess()) {
      this.dialogRef.close(true);
      this.ui.notifications.Success()
    }
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
    }
  }

}
