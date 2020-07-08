import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private searchService: SearchService) { }
  resultsGoogle: string[] =null;
  resultsBing: string[] = null;
  loadingGoogle: boolean = false;
  loadingBing: boolean = false;
  clickedOnce:boolean= false;

  getResults(searchWord: string) {
    this.loadingGoogle = true;
    this.loadingBing = true;
    this.clickedOnce = true;
    this.resultsGoogle = null;
    this.resultsBing = null;

    this.searchService.getSearchResults(searchWord, 'Google').subscribe({
      next: (results) => {this.resultsGoogle = results;  this.loadingGoogle = false;},
      error: err => console.error(err)
      
    });
    this.searchService.getSearchResults(searchWord, 'Bing').subscribe({
      next: (results) =>{ this.resultsBing = results; this.loadingBing = false;},
      error: err => console.error(err)
    });

  }

  ngOnInit(): void {
  }


}
