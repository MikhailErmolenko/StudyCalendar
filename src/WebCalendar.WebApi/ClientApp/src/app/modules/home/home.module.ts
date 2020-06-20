//import {NgModule} from '@angular/core';
//import {CommonModule} from '@angular/common';
//import {HomeComponent} from './pages/home/home.component';
//import {HomeRoutingModule} from './home-routing.module';


//@NgModule({
//  declarations: [
//    HomeComponent
//  ],
//  imports: [
//    CommonModule,
//    HomeRoutingModule
//  ]
//})
//export class HomeModule {
//}
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HomeComponent} from './pages/home/home.component';
import {HomeRoutingModule} from './home-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ReactiveFormsModule
  ]
})
export class HomeModule {
}
