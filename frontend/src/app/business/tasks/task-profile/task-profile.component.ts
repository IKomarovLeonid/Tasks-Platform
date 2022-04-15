import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {TasksMediator} from "../../../state/tasks.mediator";
import {TaskViewModel} from "../../../../communication/main.api";
import {UiService} from "../../../services/ui/ui.service";

@Component({
  selector: 'task-profile',
  templateUrl: 'task-profile.component.html',
})
export class TaskProfileComponent implements OnInit{

  public isLoading: boolean;
  public model: TaskViewModel | undefined;

  constructor(private mediator: TasksMediator, private route: ActivatedRoute, public readonly ui: UiService) {
    this.isLoading = false;
  }

  ngOnInit() {
    this.route.paramMap.subscribe(async params => {
      const id = params.get('id');
      const response = await this.mediator.GetByIdAsync(+id!);
      if(response.isSuccess()) this.model = response.data !!;
      else{
        const message = this.ui.parser.Parse(response.errorMessage);
        this.ui.notifications.Error(message);
        await this.ui.router.RedirectToHome();
      }
    })
  }

  async onRefresh(): Promise<void>{
    this.isLoading = true;
    const response = await this.mediator.GetByIdAsync(this.model?.id !!);
    if(response.isSuccess()) this.model = response.data !!;
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
      await this.ui.router.RedirectToHome();
    }
    this.isLoading = false;
  }

  async onArchive(): Promise<void>{
    const response = await this.mediator.ArchiveAsync(this.model?.id !!);
    if(response.isSuccess()) {
      this.ui.notifications.Success();
    }
    else{
      const message = this.ui.parser.Parse(response.errorMessage);
      this.ui.notifications.Error(message);
    }
    await this.ui.router.RedirectToHome();
  }
}
