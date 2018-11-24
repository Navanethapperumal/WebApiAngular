import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap, tap } from 'rxjs/operators';
import { MovieInformation, MovieProducer, MovieActor } from './MovieModel';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class MovieService {

  private movieAPIUrl = 'api/Movie/';

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
    return this.http.get<MovieProducer[]>(this.movieAPIUrl + 'producers').pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieProducer[]>('getProducers'))
    );
  }

  getActors(): Observable<MovieActor[]> {
    return this.http.get<MovieActor[]>(this.movieAPIUrl + 'actors').pipe(
      tap(_ => console.log("service called")),
      catchError(this.handleError<MovieActor[]>('getActors'))
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


