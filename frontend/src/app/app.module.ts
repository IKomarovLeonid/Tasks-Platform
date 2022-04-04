import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatSliderModule} from "@angular/material/slider";
import {MatMenuModule} from "@angular/material/menu";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatSidenavContainer, MatSidenavModule} from "@angular/material/sidenav";
import {MatButtonModule} from "@angular/material/button";
import {MatDividerModule} from "@angular/material/divider";
import {TasksViewComponent} from "./business/tasks/tasks-view/tasks-view.component";
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatInputModule} from "@angular/material/input";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [
    AppComponent,
    TasksViewComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatTableModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatPaginatorModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
