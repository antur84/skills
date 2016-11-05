import { SkillService } from './skill.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { MaterialModule } from '@angular/material';
import 'hammerjs';
import { SkillAdderComponent } from './skill-adder/skill-adder.component';
import { StarBarComponent } from './star-bar/star-bar.component';


@NgModule({
  declarations: [
    AppComponent,
    SkillAdderComponent,
    StarBarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule.forRoot()
  ],
  providers: [SkillService],
  bootstrap: [AppComponent]
})
export class AppModule { }
