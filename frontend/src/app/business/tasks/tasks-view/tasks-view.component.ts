import {AfterViewInit, Component, OnInit, ViewChild} from "@angular/core";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskViewModel, VisibleScope} from "../../../../communication/main.api";
import {MatTableDataSource} from "@angular/material/table";
import {MatDialog} from "@angular/material/dialog";
import {CreateTaskComponent} from "../tasks-create/create-task.component";
import {MatPaginator} from "@angular/material/paginator";
import {UiService} from "../../../services/ui/ui.service";
import {TaskProfileComponent} from "../task-profile/task-profile.component";

@Component({
  selector: 'tasks-view-component',
  templateUrl: 'tasks-view.component.html',
})
export class TasksViewComponent implements OnInit, AfterViewInit{

   public isLoading: boolean;
   public currentScope: VisibleScope;
   public dataSource = new MatTableDataSource<TaskViewModel>();
   public displayedColumns = ['id', 'title', 'state', 'status', 'expirationUtc', 'updatedUtc', 'profile'];

   constructor(
     private mediator: TasksMediator,
     public dialog: MatDialog,
     public readonly ui: UiService) {
     this.currentScope = VisibleScope.Active;
     this.isLoading = false;
   }

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

   async ngOnInit(): Promise<void>{
     await this.refresh();
   }

   private async refresh(): Promise<void>{
     this.isLoading = true;
     const response = await this.mediator.GetAll(this.currentScope);
     if(response.IsSuccess()) this.dataSource.data = response.items;
     else{
       const message = this.ui.parser.Parse(response.errorMessage);
       this.ui.notifications.Error(message);
     }
     this.isLoading = false;
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

   async onArchive(id: number): Promise<void> {
     await this.mediator.ArchiveAsync(id);
     await this.refresh();
   }

   onSetScope(): void{
     this.currentScope = this.currentScope == VisibleScope.Active ? VisibleScope.All : VisibleScope.Active;
     this.refresh();
   }

   onView(id: number){
     const dialogRef = this.dialog.open(TaskProfileComponent, {
       data: {
         "id": id
       }
     });

     dialogRef.afterClosed().subscribe(async result => {
       if(result) await this.refresh();
     });
   }
}
