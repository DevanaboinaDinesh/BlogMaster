import { Category } from "../../category/Models/categoriy.model";

export interface Blogpost{
    id:string;
    title:string;
    shortDescription:string;
    content:string;
    featuredImageUrl:string;
    urlHandle:string;
    author:string;
    publishedDate:Date;
    isVisible:boolean;
    categories: Category[];
}