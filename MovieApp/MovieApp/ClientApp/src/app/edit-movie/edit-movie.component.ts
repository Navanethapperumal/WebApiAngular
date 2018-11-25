import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieInformation, MovieActor, MovieProducer } from '../MovieModel';
import { MovieService } from '../movie.service';
import { Location } from '@angular/common';



@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css']
})
export class EditMovieComponent implements OnInit {
  movie: MovieInformation;
  movieActors: MovieActor[];
  movieProducers: MovieProducer[];
  selectedActors: string[] = [];
  selectedProducer: string;


  private a: number = 0;

  constructor(private movieService: MovieService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit() {

    Promise.all([this.getActors(), this.getProducers(), this.getMovie()]).then(result => {
    });
  }

  getMovie(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.movieService.getMovieById(id)
      .subscribe(result => {
        this.movie = result;
        this.selectedActors = this.movie.actors.map(a => a.name);
        this.selectedProducer = this.movie.producer.producerId;
      }, error => console.error(error));
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

  updateMoviedetails(): void {
    this.movie.actors = this.movieActors.filter(t => {
      return this.selectedActors.includes(t.name);
    });

    this.movie.producer = this.movieProducers.filter(t => t.producerId == this.selectedProducer)[0];

    this.movieService.updateMovie(this.movie)
        .subscribe(() => this.goBack());
    
  }

  goBack(): void {
    this.location.back();
  }
}
