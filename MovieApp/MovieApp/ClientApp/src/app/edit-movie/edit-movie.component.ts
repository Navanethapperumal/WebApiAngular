import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieInformation, MovieActor, MovieProducer } from '../MovieModel';
import { MovieService } from '../movie.service';


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
  
  constructor(private movieService: MovieService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getMovie();
    this.getActors();
    this.getProducers();
  }

  getMovie(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.movieService.getMovieById(id)
      .subscribe(result => {
        this.movie = result;
        //this.dataSource = result;
        this.selectedActors = this.movie.actors.map(a => a.name);
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
}
