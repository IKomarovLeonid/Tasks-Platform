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

   public currentScope: VisibleScope;
   public dataSource = new MatTableDataSource<TaskViewModel>();
   public displayedColumns = ['id', 'title', 'state', 'status', 'expirationUtc', 'updatedUtc', 'profile'];

   constructor(private mediator: TasksMediator, public dialog: MatDialog) {
     this.currentScope = VisibleScope.Active;
   }

   async ngOnInit(): Promise<void>{
     await this.refresh();
   }

   private async refresh(): Promise<void>{
     const response = await this.mediator.GetAll(this.currentScope);
     this.dataSource.data = response.items;
   }

   onCreate(): void {
     const dialogRef = this.dialog.open(CreateTaskComponent);

     dialogRef.afterClosed().subscribe(async result => {
       if(result) await this.refresh();
     });
   }

   async onRefresh(): Promise<void>{
     await this.refresh();
   }

   async onArchive(id: number): Promise<void>{
     await this.mediator.ArchiveAsync(id);
     await this.refresh();
   }

   onSetScope(): void{
     this.currentScope = this.currentScope == VisibleScope.Active ? VisibleScope.All : VisibleScope.Active;
     this.refresh();
   }
}
