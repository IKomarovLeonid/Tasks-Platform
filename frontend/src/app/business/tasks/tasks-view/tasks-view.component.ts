import {Component, OnInit} from "@angular/core";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskViewModel, VisibleScope} from "../../../../communication/main.api";
import {MatTableDataSource} from "@angular/material/table";
import {MatDialog} from "@angular/material/dialog";
import {CreateTaskComponent} from "../tasks-create/create-task.component";

@Component({
  selector: 'tasks-view-component',
  templateUrl: 'tasks-view.component.html',
})
export class TasksViewComponent implements OnInit{

   public dataSource = new MatTableDataSource<TaskViewModel>();
   public displayedColumns = ['id', 'title', 'state', 'status', 'expirationUtc', 'updatedUtc', 'profile'];

   constructor(private mediator: TasksMediator, public dialog: MatDialog) {
   }

   async ngOnInit(): Promise<void>{
     const response = await this.mediator.GetAll(VisibleScope.Active);
     console.log(response.items);
     this.dataSource.data = response.items;
   }

   onCreate(): void {
     const dialogRef = this.dialog.open(CreateTaskComponent);

     dialogRef.afterClosed().subscribe(result => {
       alert("dialog closed");
     });
   }
}
