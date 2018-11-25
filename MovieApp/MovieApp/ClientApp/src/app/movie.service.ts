import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap, tap } from 'rxjs/operators';
import { MovieInformation, MovieProducer, MovieActor } from './MovieModel';
import { Gender } from './gender.enum'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class MovieService {

  private movieAPIUrl = 'api/Movie/';
  private actorAPIUrl = 'api/Actor/';
  private producerAPIUrl = 'api/Producer/';

  constructor(private http: HttpClient) {
  }

  getMovies(): Observable<MovieInformation[]> {

    return this.http.get<MovieInformation[]>(this.movieAPIUrl).pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieInformation[]>('getMovies', []))
    );
  }

  getMovieById(id: number): Observable<MovieInformation> {
    return this.http.get<MovieInformation>(this.movieAPIUrl + id).pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieInformation>('getMovieById'))
    );
  }

  getProducers(): Observable<MovieProducer[]> {
    return this.http.get<MovieProducer[]>(this.producerAPIUrl).pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieProducer[]>('getProducers'))
    );
  }

  getActors(): Observable<MovieActor[]> {
    return this.http.get<MovieActor[]>(this.actorAPIUrl).pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieActor[]>('getActors'))
    );
  }

  createMovie(movie: MovieInformation): Observable<MovieInformation> {

    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = movie;

    return this.http.post<MovieInformation>(this.movieAPIUrl, body, { headers }).pipe(
      tap(_ => console.log(`created movie id=${movie.movieId}`)),
      catchError(this.handleError<any>('createMovie'))
    );
  }

  updateMovie(movie: MovieInformation): Observable<any> {

    const params = new HttpParams().set('id', movie.movieId);
    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = movie;

    return this.http.put<MovieInformation>(this.movieAPIUrl + movie.movieId, body, { headers }).pipe(
      tap(_ => console.log(`updated movie id=${movie.movieId}`)),
      catchError(this.handleError<any>('updateMovie'))
    );
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return observableOf(result as T);
    };
  }
}


