import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TaskProfileComponent} from "./business/tasks/task-profile/task-profile.component";
import {TasksViewComponent} from "./business/tasks/tasks-view/tasks-view.component";

const routes: Routes = [
  { path: 'tasks/:id', component: TaskProfileComponent},
  { path: '', component: TasksViewComponent},
  { path: '**', pathMatch: 'full', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
