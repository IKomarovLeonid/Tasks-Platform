import {Component, Inject, OnInit} from "@angular/core";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskViewModel} from "../../../../communication/main.api";

@Component({
  selector: 'task-profile',
  templateUrl: 'task-profile.component.html',
})
export class TaskProfileComponent implements OnInit{
  public model: TaskViewModel;

  constructor(private readonly mediator: TasksMediator, @Inject(MAT_DIALOG_DATA) public data: any) {
    this.model = new TaskViewModel();
  }

  async ngOnInit(): Promise<void>{
    const response = await this.mediator.GetByIdAsync(this.data["id"]);
    if(response.isSuccess()) this.model = response.data !!;
  }
}
