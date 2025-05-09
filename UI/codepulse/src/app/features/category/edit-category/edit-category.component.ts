import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from '../Models/categoriy.model';
import { CategoryService } from '../services/category.service';
import { UpdateCategoryRequest } from '../Models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit , OnDestroy{

  id:string | null =null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription;
  deleteCategorySubscription?: Subscription;
  category?:Category;
  constructor(private route:ActivatedRoute,private categoryService:CategoryService, private router: Router){

  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
    this.deleteCategorySubscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.paramsSubscription=this.route.paramMap.subscribe({
      next: (params)=>{
        this.id=params.get('id');
        if(this.id)
        {
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response)=>{
              this.category=response;
            }
          });
        }
      }
    });
  }
  onFormSubmit(): void{
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    // pass this object to service
    if(this.id)
    {
      this.categoryService.updateCategory(this.id,updateCategoryRequest).subscribe({
        next: (response)=>{
          this.router.navigateByUrl('/admin/categories');
        }
      })
    }
  }
  onDelete():void{
    if(this.id)
    {
      this.deleteCategorySubscription=this.categoryService.deleteCategory(this.id).subscribe({
        next: (response)=>{
          this.router.navigateByUrl('/admin/categories');
        }
      });
    }
   
  }
    

  }




