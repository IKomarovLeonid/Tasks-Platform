import {Component} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {CreateTaskRequestModel} from "../../../../communication/main.api";
import {FormlyFieldConfig} from "@ngx-formly/core";

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
      key: 'expirationUtc',
      type: 'date',
      templateOptions: {
        label: 'Expiration',
        required: false,
      }
    },

  ];

  onSubmit(model: CreateTaskRequestModel): void{
    console.log(model);
  }

}
