import {Component} from "@angular/core";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.component.html',
})
export class ConfirmDialogComponent{
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>) {
  }

  onSubmit(): void{
    this.dialogRef.close(true);
  }
}
