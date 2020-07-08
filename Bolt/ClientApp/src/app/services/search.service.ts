import { Injectable, inject, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISearchResult } from '../models/searchResult';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  getSearchResults(searchWord: string, engine): Observable<string[]> {
    return this.httpClient
      .get<string[]>(`${this.baseUrl}/api/search/${searchWord}/${engine}`);
  }
}
