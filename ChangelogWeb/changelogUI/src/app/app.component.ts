import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs/operators';
import { LoaderService } from './services/load-spinner.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  loading = false;
  showLoader = false;
  constructor(private loader: LoaderService) { }
  ngOnInit() {
    this.listenToLoading();
  }

  listenToLoading(): void {
    this.loader.loadingSub
      .pipe(delay(0))
      .subscribe((loading) => {
        this.loading = loading;
      });
  }
}
