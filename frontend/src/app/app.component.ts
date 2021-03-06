import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {JobsViewComponent} from "./business/settings/jobs/jobs-view.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  constructor(private dialog: MatDialog) {

  }

  async ngOnInit() {
    /**
    const request = new CreateTaskRequestModel();
    request.title = "From angular app";
    request.description = "From angular app description";

    const createResponse = await this.api.create(request);

    createResponse.subscribe(async data => {
      let id = data.id as number;
      const response = await this.api.getById(id);
      response.subscribe(data => {
          alert(JSON.stringify(data));
        },
        error => {
          alert(JSON.stringify(error.response))
        });
    })
     */
  }

  viewJobs(): void{
    const dialogReg = this.dialog.open(JobsViewComponent);
  }
}

