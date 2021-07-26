import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { MovieService } from '../../api/movie.service';

@UntilDestroy()
@Component({
  selector: 'app-movie-title-search',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  searchForm = new FormGroup({
    title: new FormControl(''),
  });
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.setupFormSubscription();
  }

  setupFormSubscription() {
    this.searchForm.valueChanges
      .pipe(untilDestroyed(this),
        distinctUntilChanged(),
        debounceTime(500),
      ).subscribe((changes: any) => {
        if (changes && changes.title) {
          this.movieService.movieSearch.next(changes.title);
        } else {
          this.movieService.movieSearch.next('');
        }
      });
  }

}
