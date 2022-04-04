import {Component, OnInit} from "@angular/core";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskViewModel, VisibleScope} from "../../../../communication/main.api";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'tasks-view-component',
  templateUrl: 'tasks-view.component.html',
})
export class TasksViewComponent implements OnInit{

   public dataSource = new MatTableDataSource<TaskViewModel>();
   public displayedColumns = ['id', 'title', 'status'];

   constructor(private mediator: TasksMediator) {
   }

   async ngOnInit(): Promise<void>{
     const response = await this.mediator.GetAll(VisibleScope.Active);
     console.log(response.items);
     this.dataSource.data = response.items;
   }
}
