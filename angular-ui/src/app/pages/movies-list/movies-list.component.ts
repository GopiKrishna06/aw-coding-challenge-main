import { Component, OnInit, Input } from '@angular/core';
import { Movies } from '../../models/Movies';

@Component({
  selector: 'app-movies-list',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.scss']
})
export class MoviesListComponent implements OnInit {

  @Input() moviesData = null;
  settings = {
    hideSubHeader: true,
    actions: false,
    pager: {
      display: true,
      perPage: 10
    },
    columns: {
      ID: {
        title: 'ID',
        filter: false,
        editable: false,
        addable: false,
      },
      Title: {
        filter: false,
        title: 'Title',
        editable: false,
        addable: false,
      },
      Year: {
        filter: false,
        editable: false,
        addable: false,
        title: 'Year',
      },
      Rating: {
        filter: false,
        editable: false,
        addable: false,
        title: 'Rating',
      },
    },
  };
  
  constructor() { }

  ngOnInit(): void {
  }

}
