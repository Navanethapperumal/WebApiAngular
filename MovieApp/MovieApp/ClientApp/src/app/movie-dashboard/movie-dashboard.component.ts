import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { MovieInformation } from '../MovieModel';
import { MovieService } from '../movie.service';
import { Gender } from '../gender.enum'

@Component({
  selector: 'app-movie-dashboard',
  templateUrl: './movie-dashboard.component.html',
  styleUrls: ['./movie-dashboard.component.css']
})
export class MovieDashboardComponent implements OnInit {
  movies: MovieInformation[];
  dataSource = [];

  displayedColumns: string[] = ['name', 'releaseYear', 'producer', 'actors', 'actions'];

  constructor(private movieService: MovieService) {
  }

  ngOnInit() {
    this.getMovies();
  }

  getMovies(): void {
    this.movieService.getMovies()
      .subscribe(result => {
        this.movies = result;
        //this.dataSource = result;
      }, error => console.error(error));
  }
}
