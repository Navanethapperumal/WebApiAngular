import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieInformation, MovieActor, MovieProducer } from '../MovieModel';
import { MovieService } from '../movie.service';
import { Location } from '@angular/common';
import { projectionDef } from '@angular/core/src/render3';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent implements OnInit {
  movie: MovieInformation = new MovieInformation();
  movieActors: MovieActor[];
  movieProducers: MovieProducer[];
  selectedActors: string[] = [];
  selectedProducer: string;

  private a: number = 0;

  constructor(private movieService: MovieService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit() {
    Promise.all([this.getActors(), this.getProducers()]).then(result => {
    });
  }

  getActors(): void {
    this.movieService.getActors()
      .subscribe(result => {
        this.movieActors = result;
        //this.dataSource = result;
      }, error => console.error(error));
  }

  getProducers(): void {
    this.movieService.getProducers()
      .subscribe(result => {
        this.movieProducers = result;
        //this.dataSource = result;
      }, error => console.error(error));
  }

  createMovie(): void {
    this.movie.actors = this.movieActors.filter(t => {
      return this.selectedActors.includes(t.name);
    });
    this.movie.poster = '';
    this.movie.producer = this.movieProducers.filter(t => t.producerId == this.selectedProducer)[0];

    this.movieService.createMovie(this.movie)
      .subscribe(() => this.goBack());
  }

  goBack(): void {
    this.location.back();
  }
}
