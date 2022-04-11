import {Component, Inject, OnInit} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskStatus, TaskViewModel, UpdateTaskRequestModel} from "../../../../communication/main.api";
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {UiService} from "../../../services/ui/ui.service";

@Component({
  selector: 'task-profile',
  templateUrl: 'task-profile.component.html',
})
export class TaskProfileComponent implements OnInit{
  public model: TaskViewModel;
  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [
    {
      key: 'title',
      type: 'input',
      templateOptions: {
        label: 'Task title',
        required: true,
      }
    },
    {
      key: 'description',
      type: 'input',
      templateOptions: {
        label: 'Task description',
        required: true,
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
    {
      key: 'status',
      type: 'select',
      templateOptions: {
        label: 'Current status',
        required: true,
        options: [
          {label: 'To do', value: TaskStatus.Pending},
          {label: 'In progress', value: TaskStatus.Processing},
          {label: 'Done', value: TaskStatus.Processed}
        ]
      },
    }
  ];

  constructor(
    private readonly mediator: TasksMediator,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public readonly dialog: MatDialogRef<TaskProfileComponent>,
    private readonly ui: UiService) {
    this.model = new TaskViewModel();
  }

  async ngOnInit(): Promise<void>{
    const response = await this.mediator.GetByIdAsync(this.data["id"]);
    if(response.isSuccess()) this.model = response.data !!;
  }

  async onSubmit(): Promise<void>{
    const request = new UpdateTaskRequestModel();
    request.title = this.model.title;
    request.description = this.model.description;
    request.expirationUtc = this.model.expirationUtc;
    request.status = this.model.status;

    const response = await this.mediator.UpdateAsync(this.data["id"], request);
    if(response.isSuccess()){
      this.ui.notifications.Success();
      this.dialog.close(true);
    }
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
    }
  }
}
