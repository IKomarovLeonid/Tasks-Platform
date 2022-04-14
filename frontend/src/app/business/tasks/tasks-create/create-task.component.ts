import {Component} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {CreateTaskRequestModel, Priority} from "../../../../communication/main.api";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {TasksMediator} from "../../../state/tasks.mediator";
import {MatDialogRef} from "@angular/material/dialog";
import {UiService} from "../../../services/ui/ui.service";

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
    },
    {
      key: 'priority',
      type: 'select',
      templateOptions: {
        label: 'Task priority',
        required: true,
        options: [
          {label: 'Not defined', value: Priority.NotDefined },
          {label: 'Urgent', value: Priority.Urgent },
          {label: 'High', value: Priority.High},
          {label: 'Medium', value: Priority.Medium},
          {label: 'Low', value: Priority.Low}
        ]
      },
    },
    {
      key: 'category',
      type: 'input',
      templateOptions: {
        label: 'Category (optional)',
        required: false,
      }
    },
    {
      key: 'expirationUtc',
      type: 'input',
      templateOptions: {
        label: 'Expiration time',
        type: 'date',
        required: false,
      },
    },
  ];

  constructor(
    private readonly mediator: TasksMediator,
    public dialogRef: MatDialogRef<CreateTaskComponent>,
    public readonly ui: UiService) {
  }

  async onSubmit(model: CreateTaskRequestModel): Promise<void>{
    const response = await this.mediator.CreateAsync(model);
    if(response.isSuccess()){
      this.ui.notifications.Success();
      this.dialogRef.close(true);
    }
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
    }
  }

}
