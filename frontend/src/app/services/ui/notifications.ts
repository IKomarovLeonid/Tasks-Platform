import {MatSnackBar} from "@angular/material/snack-bar";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class Notifications{
  constructor(private _snackBar: MatSnackBar) {}

  public Success(){
    this._snackBar.open('Success', '', {
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  public Error(message: string){
    this._snackBar.open(message, '', {
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }
}
