import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private searchService: SearchService) { }
  // searchWord:string;
  resultsGoogle: string[];
  resultsBing: string[];


  getResults(searchWord: string) {
    this.searchService.getSearchResults(searchWord, 'Google').subscribe({
      next: (results) => this.resultsGoogle = results,
      error: err => console.error(err)
    });
    this.searchService.getSearchResults(searchWord, 'Bing').subscribe({
      next: (results) => this.resultsBing = results,
      error: err => console.error(err)
    });
  }

  ngOnInit(): void {
  }


}
