import {Component} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {CreateTaskRequestModel} from "../../../../communication/main.api";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {TasksMediator} from "../../../state/tasks.mediator";
import {MatDialogRef} from "@angular/material/dialog";
import {UiNotificationService} from "../../../services/ui.notification.service";

@Component({
  selector: 'create-task',
  templateUrl: 'create-task.component.html',
})
export class CreateTaskComponent{
  form = new FormGroup({});
  model = new CreateTaskRequestModel();
  fields: FormlyFieldConfig[] = [
    {
      key: 'title',
      type: 'input',
      templateOptions: {
        label: 'Task title',
        placeholder: 'Enter new task title',
        required: true,
      }
    },
    {
      key: 'description',
      type: 'input',
      templateOptions: {
        label: 'What you are going to do?',
        required: true,
      }
    }
  ];

  constructor(
    private readonly mediator: TasksMediator,
    public dialogRef: MatDialogRef<CreateTaskComponent>,
    public readonly ui: UiNotificationService) {

  }

  async onSubmit(model: CreateTaskRequestModel): Promise<void>{
    const response = await this.mediator.CreateAsync(model);
    if(response.isSuccess()) this.dialogRef.close(true);
    else this.ui.onError(response.errorMessage);
  }

}
