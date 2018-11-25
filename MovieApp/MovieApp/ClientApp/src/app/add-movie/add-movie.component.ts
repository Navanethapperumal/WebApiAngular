import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieInformation, MovieActor, MovieProducer } from '../MovieModel';
import { MovieService } from '../movie.service';
import { Location } from '@angular/common';
import { Gender } from '../gender.enum'

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

  showPersonCreateSec: boolean = false;
  isMovieActor: boolean = false;

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

  displayCreateActor(): void {
    this.showPersonCreateSec = true;
    this.isMovieActor = true;

    this.scrollToPersonSection();
  }

  displayCreateProducer(): void {
    this.showPersonCreateSec = true;
    this.isMovieActor = false;

    this.scrollToPersonSection();
  }

  scrollToPersonSection(): void {
    var elmnt = document.getElementById("addPerson");
    elmnt.scrollIntoView();
  }

  visibilityChangedHandler(visibility: boolean) {
    this.showPersonCreateSec = visibility;
    console.log('person section visibility' + visibility);

    Promise.all([this.getActors(), this.getProducers()]).then(result => {
    });
  }
}
