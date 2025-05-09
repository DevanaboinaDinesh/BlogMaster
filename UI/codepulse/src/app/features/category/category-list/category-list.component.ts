import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../Models/categoriy.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit,OnDestroy{

  categories$?: Observable<Category[]>;
  totalCount? : number ;
  pageNumber = 1;
  pageSize = 3;
  list : number[] = [];

  constructor(private categoryService:CategoryService)
  {

  }
  ngOnDestroy(): void {
    this.categories$ = undefined;
  }
  ngOnInit(): void {
    
    this.categoryService.getCategoryCount().subscribe({
      next : (response) =>{
        this.totalCount = response;
        this.list = new Array(Math.ceil(response/this.pageSize));
        this.categories$=this.categoryService.getAllCategories(
          undefined,
          undefined,
          undefined,
          this.pageNumber,
          this.pageSize
        );
      }
    });

  }
  onSearch(query? : string):void{
    this.categories$=this.categoryService.getAllCategories(query);
  }
  sort(sortBy: string, sortDirection: string) : void{
    this.categories$=this.categoryService.getAllCategories(undefined,sortBy,sortDirection);
  }
  getPage(pageNumber: number) : void{
    if(pageNumber < 1 || pageNumber > this.list.length) return;
    this.pageNumber=pageNumber;
    this.categories$=this.categoryService.getAllCategories(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }
  getPreviousPage():void{
    this.getPage(this.pageNumber -1 );

  }
  getNextPage(): void{
    this.getPage(this.pageNumber + 1);

  }
}
