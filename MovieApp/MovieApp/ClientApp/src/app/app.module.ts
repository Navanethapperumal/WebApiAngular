import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatTableModule, MatFormFieldModule, MatNativeDateModule, MatInputModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';

import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MovieDashboardComponent } from './movie-dashboard/movie-dashboard.component';
import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './nav/nav.component';
import { EditMovieComponent } from './edit-movie/edit-movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { AddPersonComponent } from './add-person/add-person.component';


@NgModule({
  declarations: [
    AppComponent,
    MovieDashboardComponent,
    NavComponent,
    EditMovieComponent,
    AddMovieComponent,
    AddPersonComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatListModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
