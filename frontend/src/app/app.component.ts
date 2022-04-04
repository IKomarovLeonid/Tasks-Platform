import {Component, OnInit} from '@angular/core';
import {HealtyApi} from "../communication/main.api";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  constructor(private api: HealtyApi) {

  }

  async ngOnInit() {
    const response = await this.api.ping();
    console.log(response);
  }
}
