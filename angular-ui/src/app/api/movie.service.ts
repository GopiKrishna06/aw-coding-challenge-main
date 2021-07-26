import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, Subject } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { Movies } from '../models/Movies';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  apiURL = 'http://localhost/CodingChallenge.UI/api/movies';
  movieSearch = new Subject<any>();
  constructor(private http: HttpClient) { }

  getMoviesData(): Observable<Movies> {
    return this.http.get<Movies>(`${this.apiURL}/getMovies`)
    .pipe(
      retry(1),
      catchError(this.handleError)
    );
  }
  
  searchByTitle(title: any) : Observable<Movies> {
    if (title && title.length > 0) {
      return this.http.get<Movies>(`${this.apiURL}/searchByTitle/${title}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
    } else {
      return this.getMoviesData();
    }
  }

  handleError(error: any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
