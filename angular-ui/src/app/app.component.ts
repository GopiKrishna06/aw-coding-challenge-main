import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { MovieService } from './api/movie.service';

@UntilDestroy()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  moviesData: any;
  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.getMoviesData();
    this.searchByTitle();
  }

  getMoviesData() {
    this.movieService.getMoviesData()
      .pipe(untilDestroyed(this))
      .subscribe((response:any) => {
        this.moviesData = response;
      });
  }

  searchByTitle() {
    this.movieService.movieSearch.asObservable()
      .pipe(untilDestroyed(this))
      .subscribe((response:any) => {
          this.movieService.searchByTitle(response || '')
            .pipe(untilDestroyed(this))
            .subscribe(data => {
              this.moviesData = data;
            });
      });
  }
}
