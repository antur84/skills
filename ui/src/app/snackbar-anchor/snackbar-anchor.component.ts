import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MdSnackBar, MdSnackBarConfig } from '@angular/material';

@Component({
  selector: 'app-snackbar-anchor',
  templateUrl: './snackbar-anchor.component.html',
  styleUrls: ['./snackbar-anchor.component.css'],
  providers: [MdSnackBar]
})
export class SnackbarAnchorComponent implements OnInit {

  constructor(private snackBar: MdSnackBar, private viewContainerRef: ViewContainerRef) { }

  failedAttempt(message: string, label: string) {
    const config = new MdSnackBarConfig(this.viewContainerRef);
    this.snackBar.open(message, label, config);
  }
  ngOnInit() {
  }
}
