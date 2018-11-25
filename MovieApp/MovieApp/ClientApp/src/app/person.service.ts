import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap, tap } from 'rxjs/operators';
import { MovieInformation, MovieProducer, MovieActor } from './MovieModel';
import { Gender } from './gender.enum'

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private actorAPIUrl = 'api/Actor/';
  private producerAPIUrl = 'api/Producer/';

  constructor(private http: HttpClient) { }

  createActor(movieActor: MovieActor): Observable<MovieActor> {

    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = movieActor;

    return this.http.post<MovieActor>(this.actorAPIUrl, body, { headers }).pipe(
      tap(_ => console.log(`created actor id=${movieActor.actorId}`)),
      catchError(this.handleError<any>('createMovie'))
    );
  }

  createProducer(movieProducer: MovieProducer): Observable<MovieProducer> {

    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = movieProducer;

    return this.http.post<MovieProducer>(this.producerAPIUrl, body, { headers }).pipe(
      tap(_ => console.log(`created producer id=${movieProducer.producerId}`)),
      catchError(this.handleError<any>('createProducer'))
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
