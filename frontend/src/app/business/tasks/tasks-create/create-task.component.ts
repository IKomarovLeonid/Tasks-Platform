import {Component} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {CreateTaskRequestModel} from "../../../../communication/main.api";
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
    }
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
